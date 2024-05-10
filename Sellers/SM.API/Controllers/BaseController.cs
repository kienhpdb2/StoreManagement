using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security.Provider;

namespace SM.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected Guid? TenantId => string.IsNullOrWhiteSpace(User.Claims.FirstOrDefault(m => m.Type == "TenantId")?.Value) ? null : Guid.Parse(User.Claims.FirstOrDefault(m => m.Type == "TenantId")?.Value!);

        protected Guid AccountId => Guid.Parse(User.Claims.First()!.Value);
    }
}
