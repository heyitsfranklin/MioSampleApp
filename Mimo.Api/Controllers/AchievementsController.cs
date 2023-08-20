using System.Net;
using Microsoft.AspNetCore.Mvc;
using Mimo.Services;

namespace Mimo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AchievementsController : BaseController
{
    private readonly IAchievementLogsService _achievementLogsService;

    public AchievementsController(IAchievementLogsService achievementLogsService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _achievementLogsService = achievementLogsService;
    }

    /// <summary>
    /// Returns a list of achievements from the user context
    /// </summary>
    /// <returns>A list of achievements</returns>
    [HttpGet("me")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Get()
    {
        if (UserId == null)
            return Unauthorized();
        
        var result = await _achievementLogsService.GetAllUserAchievements((int) UserId);
        return Ok(result);
    }
}
