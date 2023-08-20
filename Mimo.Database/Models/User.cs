namespace Mimo.Database.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    
    public ICollection<AchievementLog> AchievementLogs { get; set; }
    public ICollection<ActivityLog> ActivityLogs { get; set; }
}