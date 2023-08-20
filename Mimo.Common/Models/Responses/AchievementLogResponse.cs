namespace Mimo.Common.Models.Responses;

public class AchievementLogResponse
{
    public int AchievementId { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
    public int ActivitiesCompleted { get; set; }
}