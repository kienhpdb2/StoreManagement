using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sellers.DTO;
using Sellers.Entity;
using SM.DTO.Accounts;
using SM.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Services.Repository.Account
{
    public class AccountService : IAccountService
    {
        private readonly SellerContext _context;
        private readonly SignInManager<AppUserEntity> _signInManager;
        public AccountService(SellerContext context, SignInManager<AppUserEntity> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<ResponseDto> CreateAsync(CreateAccountDto request, Guid tenantId, Guid createdBy)
        {
            var userE = await _context.Users.FirstOrDefaultAsync(m => m.Email == request.Email && !m.IsDeleted);
            if (userE != null) return new("Thông tin E-mail đã tồn tại.");

            userE = new AppUserEntity
            {
                Email = request.Email,
                FullName = request.FullName,
                EmailConfirmed = true,
                IsActived = true,
                PhoneNumber = request.PhoneNumber,
                TenantId = tenantId,
                UserName = request.Email,
                Gender = request.Gender,
                Address = request.Address,
                Dob = request.Dob,
            };
            var result = await _signInManager.UserManager.CreateAsync(userE, request.Password);
            if (result.Succeeded)
            {
                await _signInManager.UserManager.AddToRoleAsync(userE, "Member");
                return new();
            }
            else
            {
                return new("Hệ thống đang gặp vấn đề. Vui lòng thử lại sau.");
            }
        }

        public async Task<ResponseDto> DeleteAsync(string id, Guid tenantId)
        {
            var userE = await _context.Users.FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted && m.TenantId == tenantId);
            if (userE == null) return new("Không tìm thấy thông tin tài khoản.");

            userE.IsDeleted = true;
            userE.DeletedAt = DateTime.UtcNow;
            userE.LockoutEnabled = true;
            _context.Update(userE);
            await _context.SaveChangesAsync();

            return new();
        }

        public async Task<ResponseDto<List<AccountDto>>> GetAsync(Guid? tenantId)
        {

            var iQueryable = _signInManager.UserManager.Users.Include(m => m.Tenant)
                .Where(m => !m.IsDeleted && (m.Tenant == null || !m.Tenant.IsDeleted));

            if(tenantId.HasValue) {
                iQueryable = iQueryable.Where(m => m.TenantId == tenantId.Value);
            }
            
            var results = await iQueryable.Select(m => new AccountDto
            {
                Email = m.Email!,
                Id = Guid.Parse(m.Id),
                IsActived = m.IsActived,
                PhoneNumber = m.PhoneNumber!,
                TenantId = m.TenantId,
                TenantName = m.Tenant != null ? m.Tenant.Name : string.Empty,
                FullName = m.FullName
            }).ToListAsync();

            if (results != null && results.Any())
            {
                foreach(var item in results)
                {
                    var roles = await _signInManager.UserManager.GetRolesAsync(new AppUserEntity { Id = item.Id.ToString() });
                    item.Role = roles.FirstOrDefault()!;
                }
            }

            return new(results ?? new List<AccountDto>());
        }

        public async Task<ResponseDto<AccountDto>> GetByIdAsync(string id, Guid tenantId)
        {
            var iQueryable = _signInManager.UserManager.Users.Include(m => m.Tenant)
                .Where(m => !m.IsDeleted && (m.Tenant == null || !m.Tenant.IsDeleted) && m.TenantId == tenantId && m.Id == id);

            var results = await iQueryable.Select(m => new AccountDto
            {
                Email = m.Email!,
                Id = Guid.Parse(m.Id),
                IsActived = m.IsActived,
                PhoneNumber = m.PhoneNumber!,
                TenantId = m.TenantId,
                TenantName = m.Tenant != null ? m.Tenant.Name : string.Empty,
                FullName = m.FullName,
                Gender = m.Gender,
                Address = m.Address,
                Dob = m.Dob
                
            }).FirstOrDefaultAsync();

            if (results == null) return new("Không tìm thấy thông tin tài khoản.");

            var roles = await _signInManager.UserManager.GetRolesAsync(new AppUserEntity { Id = results.Id.ToString() });
            results.Role = roles.FirstOrDefault()!;

            return new(results);
        }

        public async Task<ResponseDto> UpdateAsync(EditAccountDto request, string id, Guid tenantId, Guid updatedBy)
        {
            var userE = await _context.Users.FirstOrDefaultAsync(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (userE == null) return new("Không tìm thấy thông tin tài khoản.");

            userE.PhoneNumber = request.PhoneNumber;
            userE.Address = request.Address;
            userE.FullName = request.FullName;
            userE.Dob = request.Dob;
            userE.Gender = request.Gender;

            _context.Users.Update(userE);
            await _context.SaveChangesAsync();

            return new();
        }
    }
}
