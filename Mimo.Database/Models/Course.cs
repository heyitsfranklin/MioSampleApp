namespace Mimo.Database.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int? AchievementId { get; set; }
    public bool IsDeleted { get; set; }
    
    public Achievement Achievement { get; set; }
    public ICollection<Chapter> Chapters { get; set; }
    public ICollection<ActivityLog> ActivityLogs { get; set; }
}