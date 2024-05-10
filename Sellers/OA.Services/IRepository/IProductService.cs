using Sellers.DTO;
using SM.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Services.IRepository
{
    public interface IProductService
    {
        Task<ResponseDto> CreateAsync(CreateProductDto request, Guid tenantId, Guid accountId);
        Task<ResponseDto> UpdateAsync(EditProductDto request, Guid tenantId, Guid accountId);
        Task<ResponseDto> DeleteAsync(Guid id, Guid tenantId);
        Task<ResponseDto<List<ProductDto>>> GetAsync(Guid tenantId);
        Task<ResponseDto<ProductDto>> GetByIdAsync(Guid id, Guid tenantId);
    }
}
