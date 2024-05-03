using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sellers.DTO;
using Sellers.DTO.Auths;
using Sellers.Entity;
using Sellers.Services.IRepository;
using SM.DTO.Auths;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sellers.Services.Repository.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<AppUserEntity> _signInManager;
        private readonly SellerContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(SignInManager<AppUserEntity> signInManager, SellerContext sellerContext, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _context = sellerContext;
            _configuration = configuration;
        }

        public async Task<ResponseDto<LoginResultsDto>> SignInAsync(LoginDto model, bool shouldLockout)
        {
            try
            {
                if (_signInManager != null && _signInManager.UserManager != null)
                {
                    var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);
                    if (user != null && user.IsActived && user.IsDeleted == false)
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            var roles = await _signInManager.UserManager.GetRolesAsync(user);

                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtAuthentication:Key")!);
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()!),
                                    new Claim("TenantId", user.TenantId.HasValue ? user.TenantId.ToString()! : "")
                                }),
                                Expires = DateTime.Now.AddHours(1),
                                Audience = _configuration.GetValue<string>("JwtAuthentication:Audience"),
                                Issuer = _configuration.GetValue<string>("JwtAuthentication:Issuer"),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                            };
                            var token = tokenHandler.CreateToken(tokenDescriptor);
                            var jwtToken = tokenHandler.WriteToken(token);
                            var loginResult = new LoginResultsDto { Token = jwtToken };
                            return new(loginResult);
                        }
                    }
                }
                return new("Thông tin tài khoản không đúng");
            }
            catch (Exception ex) { return new(ex.Message); }
        }

        public async Task<ResponseDto<UsersDto>> GetUserById(string id)
        {
            var userE = await _signInManager.UserManager.FindByIdAsync(id);
            if (userE == null) return new("Không tồn tại thông tin người dùng.");
            var roles = await _signInManager.UserManager.GetRolesAsync(userE);
            var userDto = new UsersDto
            {
                FullName = "",
                Id = userE.Id,
                StoreName = "",
                Role = roles.First()
            };
            return new(userDto);
        }


        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ResponseDto> SignUpAsync(string email, string password, Guid tenantId, string role, string phoneNumber, string fullName)
        {
            var usersEntity = new AppUserEntity { PhoneNumber = phoneNumber, FullName = fullName, IsActived = true, Email = email, UserName = email, TenantId = tenantId };
            var result = await _signInManager.UserManager.CreateAsync(usersEntity, password);
            if (result.Succeeded)
            {
                await _signInManager.UserManager.AddToRoleAsync(usersEntity, role);
                return new();
            }
            else
            {
                return new("Hệ thống đang gặp vấn đề. Vui lòng thử lại sau.");
            }
        }
    }
}
