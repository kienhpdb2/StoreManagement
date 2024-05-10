using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SM.Services.IRepository;
using SM.Services.Repository.Account;

namespace SM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Member, Tenant")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var results = await _accountService.GetAsync(TenantId);

            return new ObjectResult(results);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateAccountDto request)
        {
            var result = await _accountService.CreateAsync(request, TenantId!.Value, AccountId);
            return new ObjectResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] EditAccountDto request, string id)
        {
            var result = await _accountService.UpdateAsync(request, id, TenantId!.Value, AccountId);
            return new ObjectResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _accountService.GetByIdAsync(id, TenantId!.Value);
            return new ObjectResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _accountService.DeleteAsync(id, TenantId!.Value);
            return new ObjectResult(result);
        }
    }
}
