using Microsoft.EntityFrameworkCore;
using Sellers.DTO;
using Sellers.Entity;
using Sellers.Services.IRepository;
using SM.DTO.Tenants;
using SM.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Services.Repository.Tenants
{
    public class TenantService : ITenantService
    {
        private readonly SellerContext _context;
        private readonly IAuthenticationService _auService;
        public TenantService(SellerContext context, IAuthenticationService authenticationService)
        {
            _context = context;
            _auService = authenticationService;
        }
        public async Task<ResponseDto> CreateAsync(CreateTenantDto request)
        {
            var tenantE = await _context.Tenants.FirstOrDefaultAsync(m => m.Email == request.Email && !m.IsDeleted);
            if (tenantE != null) return new("Thông tin E-mail đã tồn tại.");

            tenantE = new Entity.TenantEntity
            {
                Email = request.Email,
                Description = request.Description,
                Id = Guid.NewGuid(),
                Address = request.Address,
                CreatedAt = DateTime.UtcNow,
                IsActived = true,
                Name = request.Name,
                Phone = request.Phone,
                StoreOwnerName = request.StoreOwnerName,
                UpdatedAt = DateTime.UtcNow,
            };
            await _context.Tenants.AddAsync(tenantE);
            await _context.SaveChangesAsync();

            /*** Create Account Tenant Admin */
            var result = await _auService.SignUpAsync(tenantE.Email, request.Password, tenantE.Id, "Tenant", request.Phone, request.StoreOwnerName);
            return result;
        }

        public async Task<ResponseDto<List<TenantDto>>> GetAsync()
        {
            var iQueryable = _context.Tenants.Where(m => !m.IsDeleted);

            var results = await iQueryable.Select(m => new TenantDto
            {
                IsDeleted = m.IsDeleted,
                Address = m.Address,
                CreatedAt = m.CreatedAt,
                DeletedAt = m.DeletedAt,
                Description = m.Description,
                Email = m.Email,
                Id = m.Id,
                IsActived = m.IsActived,
                Name = m.Name,
                Phone = m.Phone,
                StoreOwnerName = m.StoreOwnerName,
                UpdatedAt = m.UpdatedAt
            }).ToListAsync();

            return new(results);
        }

        public async Task<ResponseDto<TenantDto>> GetByIdAsync(Guid id)
        {
            var iQueryable = _context.Tenants.Where(m => m.Id == id && !m.IsDeleted);

            var result = await iQueryable.Select(m => new TenantDto
            {
                Id = m.Id,
                Address = m.Address,
                CreatedAt = m.CreatedAt,
                Description = m.Description,
                Email = m.Email,
                IsActived = m.IsActived,
                Name = m.Name,
                Phone = m.Phone,
                StoreOwnerName = m.StoreOwnerName,
                UpdatedAt = m.UpdatedAt
            }).FirstOrDefaultAsync();
            if (result == null) return new("Cửa hàng không tồn tại.");

            return new(result);
        }

        public async Task<ResponseDto> UpdateAsync(EditTenantDto request, Guid id)
        {
            var tenantE = await _context.Tenants.FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
            if (tenantE == null) return new("Không tồn tại thông tin cửa hàng.");

            tenantE.UpdatedAt = DateTime.UtcNow;
            tenantE.Name = request.Name;
            tenantE.Phone = request.Phone;
            tenantE.Description = request.Description;
            tenantE.Address = request.Address;
            tenantE.StoreOwnerName = request.StoreOwnerName;

            _context.Tenants.Update(tenantE);

            await _context.SaveChangesAsync();
            return new();
        }

        public async Task<ResponseDto> RemoveAsync(Guid id)
        {
            var tenantE = await _context.Tenants.FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
            if (tenantE == null) return new("Không tồn tại thông tin cửa hàng");

            tenantE.DeletedAt = DateTime.UtcNow;
            tenantE.IsDeleted = true;

            _context.Tenants.Update(tenantE);
            await _context.SaveChangesAsync();

            return new();
        }
    }
}
