using Microsoft.AspNetCore.Mvc;

namespace Mimo.Api.Controllers;

public class BaseController : ControllerBase
{
    protected readonly int? UserId;

    public BaseController(IHttpContextAccessor httpContextAccessor)
    {
        // get/store userid from session/jwt
        // var userIdValue = httpContextAccessor.HttpContext.Session.GetString("UserId");
        // UserId = !string.IsNullOrEmpty(userIdValue) && int.TryParse(userIdValue, out var userId)? userId :  null;
        UserId = 1;
    }
}