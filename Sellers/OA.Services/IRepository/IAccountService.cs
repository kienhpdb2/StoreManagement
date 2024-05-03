using Sellers.DTO;
using SM.DTO.Accounts;
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
    }
}
