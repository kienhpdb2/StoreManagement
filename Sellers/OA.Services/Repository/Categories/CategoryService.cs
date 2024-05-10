using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sellers.DTO;
using Sellers.Entity;
using SM.DTO.Categories;
using SM.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Services.Repository.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly SellerContext _sellerContext;
        public CategoryService(SellerContext sellerContext)
        {
            _sellerContext = sellerContext;
        }
        public async Task<ResponseDto> CreateAsync(CreateCategoryDto request, Guid tenantId, Guid accountId)
        {
            var categoryE = await _sellerContext.Categories.FirstOrDefaultAsync(m => m.Name == request.Name && !m.IsDeleted && m.TenantId == tenantId);
            if (categoryE != null) return new("Tên thể loại sản phẩm đã tồn tại.");
            categoryE = new Entity.CategoryEntity
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                Description = request.Description,
                IsActived = true,
                TenantId = tenantId,
                UpdatedAt = DateTime.UtcNow,
                AccountId = accountId.ToString(),
            };
            await _sellerContext.Categories.AddAsync(categoryE);
            await _sellerContext.SaveChangesAsync();
            return new();
        }

        public async Task<ResponseDto> DeleteAsync(Guid id, Guid tenantId)
        {
            var categoryE = await _sellerContext.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (categoryE == null) return new("Thể loại sản phẩm không tồn tại");

            if (categoryE.TenantId != tenantId) return new("Bạn không có quyền xóa thể loại sản phẩm này.");
            
            categoryE.IsDeleted = true;
            categoryE.DeletedAt = DateTime.UtcNow;
            _sellerContext.Update(categoryE);
            await _sellerContext.SaveChangesAsync();
            return new();
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAsync(Guid tenantId)
        {
            var iQueryable = _sellerContext.Categories.Include(m => m.Tenant).Include(m => m.Account)
                 .Where(m => !m.IsDeleted && m.TenantId == tenantId);

            var results = await iQueryable.Select(m => new CategoryDto
            {
                CreatedAt = m.CreatedAt,
                Description = m.Description,
                Id = m.Id,
                IsActived = m.IsActived,
                Name = m.Name,
                TenantName = m.Tenant.Name,
                CreatedName = m.Account.FullName
            }).ToListAsync();

            return new(results);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(Guid id, Guid tenantId)
        {
            var iQueryable = _sellerContext.Categories.Include(m => m.Tenant).Include(m => m.Account).Where(m => !m.IsDeleted && m.Id == id && m.TenantId == tenantId);

            var result = await iQueryable.Select(m => new CategoryDto
            {
                CreatedAt = m.CreatedAt,
                Description = m.Description,
                Name = m.Name,
                Id = m.Id,
                IsActived = m.IsActived,
                TenantName = m.Tenant.Name,
                CreatedName = m.Account.FullName,
            }).FirstOrDefaultAsync();

            if (result == null) return new("Không tìm thấy thể loại sản phẩm.");

            return new(result);
        }

        public async Task<ResponseDto> UpdateAsync(EditCategoryDto request, Guid id, Guid tenantId)
        {
            var categoryE = await _sellerContext.Categories.FirstOrDefaultAsync(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (categoryE == null) return new("Thể loại sản phẩm không tồn tại.");

            categoryE.Name = request.Name;
            categoryE.Description = request.Description;

            _sellerContext.Update(categoryE);
            await _sellerContext.SaveChangesAsync();
            return new();
        }
    }
}
