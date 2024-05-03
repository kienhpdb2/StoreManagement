using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellers.Entity.Seed
{
    public class InitializeSeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SellerContext(serviceProvider.GetRequiredService<DbContextOptions<SellerContext>>()))
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUserEntity>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userRoleNames = new List<string> { "Admin", "Member", "Tenant" };

                foreach (var roleName in userRoleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var users = new List<AppUserEntity>
                {
                    new AppUserEntity { UserName = "admin@gmail.com", Email = "admin@gmail.com", IsActived = true },
                };

                foreach (var user in users)
                {
                    if (userManager.FindByEmailAsync(user.Email!).Result == null)
                    {
                        await userManager.CreateAsync(user!, "Admin123!");
                        await userManager.AddToRoleAsync(user!, "Admin");
                    }
                }
            }
        }
    }
}
