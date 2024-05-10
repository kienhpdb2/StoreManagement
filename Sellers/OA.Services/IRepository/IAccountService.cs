using Sellers.DTO;
using SM.DTO.Accounts;
using SM.Services.Repository.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Services.IRepository
{
    public interface IAccountService
    {
        Task<ResponseDto<List<AccountDto>>> GetAsync(Guid? tenantId);
        Task<ResponseDto> CreateAsync(CreateAccountDto request, Guid tenantId, Guid createdBy);
        Task<ResponseDto> UpdateAsync(EditAccountDto request, string id, Guid tenantId, Guid updatedBy);
        Task<ResponseDto> DeleteAsync(string id, Guid tenantId);
        Task<ResponseDto<AccountDto>> GetByIdAsync(string id, Guid tenantId);
    }
}
