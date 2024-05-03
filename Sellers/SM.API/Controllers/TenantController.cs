using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SM.DTO.Tenants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Sellers.DTO;
using SM.Services.IRepository;

namespace SM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTenantDto request)
        {
            var result = await _tenantService.CreateAsync(request);
            return new ObjectResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] EditTenantDto request, Guid id)
        {
            var result = await _tenantService.UpdateAsync(request, id);
            return new ObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _tenantService.GetAsync();
            return new ObjectResult(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _tenantService.GetByIdAsync(id);
            return new ObjectResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            var result = await _tenantService.RemoveAsync(id);
            return new ObjectResult(result);
        }
    }
}
