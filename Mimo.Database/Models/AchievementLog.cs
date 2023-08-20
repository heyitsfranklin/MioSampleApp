using Microsoft.EntityFrameworkCore;

namespace Mimo.Database.Models;

[PrimaryKey(nameof(AchievementId), nameof(UserId))]
public class AchievementLog
{
    public int AchievementId { get; set; }
    public int UserId { get; set; }
    
    public Achievement Achievement { get; set; }
    public User User { get; set; }
}
