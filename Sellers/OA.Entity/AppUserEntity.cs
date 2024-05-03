using Microsoft.AspNetCore.Identity;
using SM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellers.Entity
{
    public class AppUserEntity : IdentityUser
    {
        public bool IsDeleted { get; set; }
        public bool IsActived { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? TenantId { get; set; }
        public string FullName { get; set; }

        public TenantEntity? Tenant { get; set; }
    }
}
