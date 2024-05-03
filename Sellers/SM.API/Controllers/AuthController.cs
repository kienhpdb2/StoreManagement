using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sellers.DTO.Auths;
using Sellers.Services.IRepository;

namespace SM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var result = await _authenticationService.SignInAsync(request, false);
            return new ObjectResult(result);
        }

        [Authorize]
        [HttpGet(nameof(Me))]
        public async Task<IActionResult> Me()
        {
            var result = await _authenticationService.GetUserById(User.Claims.First().Value);
            return new ObjectResult(result);
        }
    }
}
