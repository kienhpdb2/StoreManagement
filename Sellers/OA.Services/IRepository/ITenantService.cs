using Sellers.DTO;
using SM.DTO.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Services.IRepository
{
    public interface ITenantService
    {
        Task<ResponseDto> CreateAsync(CreateTenantDto request);
        Task<ResponseDto> UpdateAsync(EditTenantDto request, Guid id);
        Task<ResponseDto<TenantDto>> GetByIdAsync(Guid id);
        Task<ResponseDto<List<TenantDto>>> GetAsync();
        Task<ResponseDto> RemoveAsync(Guid id);
    }
}
