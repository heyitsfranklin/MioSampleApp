using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Mimo.Common.Enums;
using Mimo.Common.Models.Responses;
using Mimo.Database;
using Mimo.Database.Models;

namespace Mimo.Services;

public class AchievementLogsService : IAchievementLogsService
{
    private readonly DatabaseContext _databaseContext;

    public AchievementLogsService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<IEnumerable<AchievementLogResponse>> GetAllUserAchievements(int userId)
    {
        // get all the achievements that have already been obtained
        var groupings = await _databaseContext
            .Achievements
            .Include(a => a.Criteria)
            .GroupJoin(
                _databaseContext.AchievementLogs.Where(l => l.UserId == userId),
                a => a.Id,
                al => al.AchievementId,
                ((achievement, log) => new { Achievement = achievement, Log = log }))
            .SelectMany(achievementAndLog => achievementAndLog.Log.DefaultIfEmpty(),
                (a, l) => new { a.Achievement, Log = l })
            .GroupBy(achievementAndLog => achievementAndLog.Achievement)
            .ToDictionaryAsync(x => x.Key, x => x.Select(e => e.Log));

        // calculate the total number of activities that have been completed per each achievement
        var groupedAchievements = groupings
            .ToDictionary(g => g.Key, g => new AchievementLogResponse
            {
                AchievementId = g.Key.Id,
                Title = g.Key.Title,
                IsCompleted = g.Value.Any(l => l != null),
                ActivitiesCompleted = g.Value.Count(achievementAndLog => achievementAndLog != null)
            });
        
        // get the current progress of un-obtained achievements
        await GetProgressOfMissingAchievements(groupedAchievements, userId);

        return groupedAchievements
            .Select(g => g.Value);
    }
    private async Task GetProgressOfMissingAchievements(Dictionary<Achievement, AchievementLogResponse> allAchievements, int userId)
    {
        foreach (var achievementGroup in allAchievements.Where(g => !g.Value.IsCompleted))
        {
            // get logs from any activity that would count towards earning this achievement
            var logs =
                (await _databaseContext.ActivityLogs
                    .Where(l => l.UserId == userId)
                    .Where(l => l.CompletedDate != null)
                    // The ToListAsync() call below is very far from ideal but it is needed as a workaround to a confirmed bug in EF Core 7 translating nullable data types to sql...
                    // The workaround allows the final .Where() statement to be evaluated client side so that the nullable data types (RequiredCourseId and Type) can be evaluated
                    // In a production environment I would find a better workaround or (for new apps) consider if targeting .NET 6 + EF Core 6 would be better and upgrading to v7 later...
                    // See https://github.com/dotnet/efcore/issues/31081#issuecomment-1594155490 for details
                    .ToListAsync())
                .Where(log =>
                    achievementGroup.Key.Criteria.Any(criterion => criterion.RequiredCourseId != null && criterion.RequiredCourseId == log.CourseId) ||
                    (achievementGroup.Key.Criteria.Any(criterion => criterion.RequiredActivityType == ActivityType.Course) && log.CourseId != null) ||
                    (achievementGroup.Key.Criteria.Any(criterion => criterion.RequiredActivityType == ActivityType.Chapter) && log.ChapterId != null) ||
                    (achievementGroup.Key.Criteria.Any(criterion => criterion.RequiredActivityType == ActivityType.Lesson) && log.LessonId != null));

            achievementGroup.Value.ActivitiesCompleted = logs.Count();
        }
    }
}

public interface IAchievementLogsService
{
    Task<IEnumerable<AchievementLogResponse>> GetAllUserAchievements(int userId);
}