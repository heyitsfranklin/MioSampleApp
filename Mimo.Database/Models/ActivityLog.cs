using System.ComponentModel.DataAnnotations.Schema;

namespace Mimo.Database.Models;

public class ActivityLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? CourseId { get; set; }
    public int? ChapterId { get; set; }
    public int? LessonId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? CompletedDate { get; set; }
    [NotMapped] 
    public bool IsCompleted => CompletedDate != null;
    
    public User User { get; set; }
    public Course? Course { get; set; }
    public Chapter? Chapter { get; set; }
    public Lesson? Lesson { get; set; }
}
