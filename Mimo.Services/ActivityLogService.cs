using Microsoft.EntityFrameworkCore;
using Mimo.Common.Enums;
using Mimo.Common.Models.Requests;
using Mimo.Database;
using Mimo.Database.Models;

namespace Mimo.Services;

public class ActivityLogService : IActivityLogService
{
    private readonly DatabaseContext _databaseContext;

    public ActivityLogService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ActivityLog?> GetByLogType(ActivityType type, int activityId)
    {
        return await _databaseContext.ActivityLogs
            .FirstOrDefaultAsync(l => 
                (type == ActivityType.Lesson && l.LessonId == activityId) ||
                (type == ActivityType.Chapter && l.ChapterId == activityId) ||
                (type == ActivityType.Course && l.CourseId == activityId));
    }
    
    public async Task MarkCompleted(ActivityLog dbActivityLog, DateTimeOffset completedDate)
    {
        // this db design requires a StartDate when creating an ActivityLog...
        // attempts to change the start date are ignored as there should be no reason to change this date
        dbActivityLog.CompletedDate = completedDate;
        await _databaseContext.SaveChangesAsync();
    }
}

public interface IActivityLogService
{
    Task<ActivityLog?> GetByLogType(ActivityType type, int activityId);
    Task MarkCompleted(ActivityLog dbActivityLog, DateTimeOffset completedDate);
}