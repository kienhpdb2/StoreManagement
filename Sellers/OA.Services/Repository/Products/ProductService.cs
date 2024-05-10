using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sellers.DTO;
using Sellers.Entity;
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
                AccountId = accountId,
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

            }).ToListAsync();

            return new(results);
        }

        public Task<ResponseDto<ProductDto>> GetByIdAsync(Guid id, Guid tenantId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> UpdateAsync(EditProductDto request, Guid tenantId, Guid accountId)
        {
            throw new NotImplementedException();
        }
    }
}
