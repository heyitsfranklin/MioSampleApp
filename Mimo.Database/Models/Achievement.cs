using System.ComponentModel.DataAnnotations.Schema;

namespace Mimo.Database.Models;

public class Achievement
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public ICollection<AchievementCriterion> Criteria { get; set; }
    public ICollection<AchievementLog> Activities { get; set; }
}
