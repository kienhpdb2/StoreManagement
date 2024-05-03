using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sellers.DTO;
using Sellers.Entity;
using SM.DTO.Accounts;
using SM.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Services.Repository.Account
{
    public class AccountService : IAccountService
    {
        private readonly SellerContext _context;
        private readonly SignInManager<AppUserEntity> _signInManager;
        public AccountService(SellerContext context, SignInManager<AppUserEntity> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        public async Task<ResponseDto<List<AccountDto>>> GetAsync(Guid? tenantId)
        {

            var iQueryable = _signInManager.UserManager.Users.Include(m => m.Tenant)
                .Where(m => !m.IsDeleted && (m.Tenant == null || !m.Tenant.IsDeleted));

            if(tenantId.HasValue) {
                iQueryable = iQueryable.Where(m => m.TenantId == tenantId.Value);
            }
            
            var results = await iQueryable.Select(m => new AccountDto
            {
                Email = m.Email!,
                Id = Guid.Parse(m.Id),
                IsActived = m.IsActived,
                PhoneNumber = m.PhoneNumber!,
                TenantId = m.TenantId,
                TenantName = m.Tenant != null ? m.Tenant.Name : string.Empty,
                FullName = m.FullName
            }).ToListAsync();

            if (results != null && results.Any())
            {
                foreach(var item in results)
                {
                    var roles = await _signInManager.UserManager.GetRolesAsync(new AppUserEntity { Id = item.Id.ToString() });
                    item.Role = roles.FirstOrDefault()!;
                }
            }

            return new(results ?? new List<AccountDto>());
        }
    }
}
