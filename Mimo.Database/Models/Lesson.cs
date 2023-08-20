using Microsoft.EntityFrameworkCore;

namespace Mimo.Database.Models;

[Index(nameof(ChapterId), nameof(DisplayOrder), IsUnique = true)]
public class Lesson
{
    public int Id { get; set; }
    public int ChapterId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsDeleted { get; set; }

    public Chapter Chapter { get; set; }
    public ICollection<ActivityLog> ActivityLogs { get; set; }
}