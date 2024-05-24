using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sellers.DTO;
using Sellers.Entity;
using SM.DTO.Categories;
using SM.DTO.Products;
using SM.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Services.Repository.Products
{
    public class ProductService : IProductService
    {
        private readonly SellerContext _sellerContext;
        public ProductService(SellerContext sellerContext)
        {
            _sellerContext = sellerContext;
        }
        public async Task<ResponseDto> CreateAsync(CreateProductDto request, Guid tenantId, Guid accountId)
        {
            var productE = await _sellerContext.Products.FirstOrDefaultAsync(m => m.Name == request.Name && !m.IsDeleted && m.TenantId == tenantId);

            if (productE != null) return new("Tên sản phẩm đã tồn tại.");

            productE = new Entity.ProductEntity
            {
                TenantId = tenantId,
                AccountId = accountId!.ToString(),
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PurchasePrice = request.PurchasePrice,
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                IsActived = true,
                UpdatedAt = DateTime.UtcNow,
            };
            await _sellerContext.Products.AddAsync(productE);
            await _sellerContext.SaveChangesAsync();

            return new();
        }

        public async Task<ResponseDto> DeleteAsync(Guid id, Guid tenantId)
        {
            var productE = await _sellerContext.Products.FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted && m.TenantId == tenantId);
            if (productE == null) return new("Sản phẩm không tồn tại");

            productE.IsDeleted = true;
            productE.DeletedAt = DateTime.UtcNow;

            _sellerContext.Update(productE);
            await _sellerContext.SaveChangesAsync();

            return new();
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAsync(Guid tenantId)
        {
            var iQueryable = _sellerContext.Products.Include(m => m.Tenant).Include(m => m.Category)
                .Include(m => m.Account)
                .Where(m => !m.IsDeleted && m.TenantId == tenantId);

            var results = await iQueryable.Select(m => new ProductDto
            {
                CategoryId = m.CategoryId,
                CategoryName = m.Category.Name,
                Name = m.Name,
                Price = m.Price,
                CreatedAt = m.CreatedAt,
                CreatedName = m.Account.FullName,
                Description = m.Description,
                Id = m.Id,
                PurchasePrice = m.PurchasePrice,
                TenantName = m.Tenant.Name,
            }).ToListAsync();

            return new(results);
        }

        public async Task<ResponseDto<List<ProductDto>>> SearchProductAsync(Guid tenantId, string keyword)
        {
            var iQueryable = _sellerContext.Products.Include(m => m.Tenant).Include(m => m.Category)
                .Include(m => m.Account)
                .Where(m => !m.IsDeleted && m.TenantId == tenantId);
            if(!string.IsNullOrWhiteSpace(keyword))
            {
                iQueryable = iQueryable.Where(m => EF.Functions.Like(m.Name.ToLower(), $"%{keyword.ToLower()}%"));
            }

            var results = await iQueryable.Select(m => new ProductDto
            {
                CategoryId = m.CategoryId,
                CategoryName = m.Category.Name,
                Name = m.Name,
                Price = m.Price,
                CreatedAt = m.CreatedAt,
                CreatedName = m.Account.FullName,
                Description = m.Description,
                Id = m.Id,
                PurchasePrice = m.PurchasePrice,
                TenantName = m.Tenant.Name,
                Label = m.Name,
                Value = m.Name,
            }).ToListAsync();

            return new(results);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(Guid id, Guid tenantId)
        {
            var iQueryable = _sellerContext.Products.Include(m => m.Tenant).Include(m => m.Account).Where(m => !m.IsDeleted && m.Id == id && m.TenantId == tenantId);

            var result = await iQueryable.Select(m => new ProductDto
            {
                CategoryId = m.CategoryId,
                CategoryName = m.Category.Name,
                Name = m.Name,
                Price = m.Price,
                CreatedAt = m.CreatedAt,
                CreatedName = m.Account.FullName,
                Description = m.Description,
                Id = m.Id,
                PurchasePrice = m.PurchasePrice,
                TenantName = m.Tenant.Name,
            }).FirstOrDefaultAsync();

            if (result == null) return new("Không tìm thấy sản phẩm.");

            return new(result);
        }

        public async Task<ResponseDto> UpdateAsync(EditProductDto request, Guid id, Guid tenantId)
        {
            var productE = await _sellerContext.Products.FirstOrDefaultAsync(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (productE == null) return new("Sản phẩm không tồn tại.");

            productE.Name = request.Name;
            productE.Description = request.Description;
            productE.PurchasePrice = request.PurchasePrice;
            productE.Price = request.Price;
            productE.CategoryId = request.CategoryId;

            _sellerContext.Update(productE);
            await _sellerContext.SaveChangesAsync();
            return new();
        }
    }
}
