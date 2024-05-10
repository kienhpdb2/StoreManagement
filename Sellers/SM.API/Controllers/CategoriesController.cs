using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SM.DTO.Categories;
using SM.Services.IRepository;

namespace SM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Member, Tenant")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var results = await _categoryService.GetAsync(TenantId!.Value);

            return new ObjectResult(results);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCategoryDto request)
        {
            var result = await _categoryService.CreateAsync(request, TenantId!.Value, AccountId);

            return new ObjectResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _categoryService.GetByIdAsync(id, TenantId!.Value);
            return new ObjectResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] EditCategoryDto request, Guid id)
        {
            var result = await _categoryService.UpdateAsync(request, id, TenantId!.Value);
            return new ObjectResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id, TenantId!.Value);
            return new ObjectResult(result);
        }
    }
}
