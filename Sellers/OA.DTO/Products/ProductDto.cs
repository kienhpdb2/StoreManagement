using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.DTO.Products
{
    public class ProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string TenantName { get; set; }
        public string CreatedName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
