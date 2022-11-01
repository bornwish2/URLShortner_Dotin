using Infrastructures.ApplicationServices.WebFramework.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructures.ApplicationServices.WebFramework.Api
{
    [ApiController]
    [AllowAnonymous]
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]")]// api/v1/[controller]
    public class BaseController : ControllerBase
    {
        ////public UserRepository UserRepository { get; set; } => property injection
        //public bool UserIsAutheticated => HttpContext.User.Identity.IsAuthenticated;
    }
}
