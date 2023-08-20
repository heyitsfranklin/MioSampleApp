using System.Net;
using Microsoft.AspNetCore.Mvc;
using Mimo.Common.Models.Requests;
using Mimo.Services;

namespace Mimo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityLogController : BaseController
{
    private readonly IActivityLogService _activityLogService;

    public ActivityLogController(IActivityLogService activityLogService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _activityLogService = activityLogService;
    }

    /// <summary>
    /// Updates an activity log (marks an activity completed)
    /// </summary>
    /// <param name="request">The activity parameters to update</param>
    [HttpPut("complete")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Complete([FromBody] ActivityLogCompletedRequest request)
    {
        var dbActivityLog = await _activityLogService.GetByLogType(request.ActivityType, request.ActivityId);
        if (dbActivityLog == null)
            return NotFound();

        if (dbActivityLog.UserId != UserId)
            return Unauthorized();

        if (dbActivityLog.IsCompleted)
            return BadRequest("Activity has already been completed");

        await _activityLogService.MarkCompleted(dbActivityLog, request.CompletedDate);
        return Ok();
    }
}
