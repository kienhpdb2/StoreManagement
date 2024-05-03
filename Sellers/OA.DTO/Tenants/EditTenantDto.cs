using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.DTO.Tenants
{
    public class EditTenantDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string StoreOwnerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
