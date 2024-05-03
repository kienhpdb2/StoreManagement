using Sellers.DTO;
using Sellers.DTO.Auths;
using SM.DTO.Auths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellers.Services.IRepository
{
    public interface IAuthenticationService
    {
        Task<ResponseDto<LoginResultsDto>> SignInAsync(LoginDto model, bool shouldLockout);
        Task SignOutAsync();
        Task<ResponseDto<UsersDto>> GetUserById(string id);
        Task<ResponseDto> SignUpAsync(string email, string password, Guid tenantId, string role, string phoneNumber, string fullName);
    }
}
