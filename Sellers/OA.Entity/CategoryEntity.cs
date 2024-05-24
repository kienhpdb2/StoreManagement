using Sellers.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Entity
{
    [Table("Categories")]
    public class CategoryEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActived { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
        public string AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public AppUserEntity Account { get; set; }

        public TenantEntity Tenant { get; set; }

        public ICollection<ProductEntity> Products { get; set; }
    }
}
