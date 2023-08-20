using Microsoft.EntityFrameworkCore;

namespace Mimo.Database.Models;

[Index(nameof(CourseId), nameof(DisplayOrder), IsUnique = true)]
public class Chapter
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsDeleted { get; set; }

    public Course Course { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
    public ICollection<ActivityLog> ActivityLogs { get; set; }
}
