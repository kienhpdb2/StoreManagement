using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellers.Entity
{
    public class SellerContext : IdentityDbContext<AppUserEntity>
    {
        public SellerContext(DbContextOptions<SellerContext> options) : base(options)
        {
        }

        public DbSet<TenantEntity> Tenants { get; set; }
    }
}
