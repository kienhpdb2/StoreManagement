using Sellers.DTO;
using SM.DTO.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Services.IRepository
{
    public interface ICategoryService
    {
        Task<ResponseDto> CreateAsync(CreateCategoryDto request, Guid tenantId, Guid accountId);
        Task<ResponseDto> UpdateAsync(EditCategoryDto request, Guid id, Guid tenantId);
        Task<ResponseDto> DeleteAsync(Guid id, Guid tenantId);
        Task<ResponseDto<List<CategoryDto>>> GetAsync(Guid tenantId);
        Task<ResponseDto<CategoryDto>> GetByIdAsync(Guid id, Guid tenantId);
    }
}
