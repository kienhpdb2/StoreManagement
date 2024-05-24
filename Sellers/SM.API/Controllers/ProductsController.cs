using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SM.DTO.Products;
using SM.Services.IRepository;
using SM.Services.Repository.Categories;

namespace SM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Member, Tenant")]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductDto request)
        {
            var result = await _productService.CreateAsync(request, TenantId!.Value, AccountId);
            return new ObjectResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] EditProductDto request, Guid id)
        {
            var result = await _productService.UpdateAsync(request, id, TenantId!.Value);
            return new ObjectResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _productService.GetByIdAsync(id, TenantId!.Value);
            return new ObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var results = await _productService.GetAsync(TenantId!.Value);

            return new ObjectResult(results);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProductAsync([FromQuery] string keyword)
        {
            var results = await _productService.SearchProductAsync(TenantId!.Value, keyword);

            return new ObjectResult(results);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            var result = await _productService.DeleteAsync(id, TenantId!.Value);
            return new ObjectResult(result);
        }
    }
}
