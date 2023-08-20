using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Mimo.Common.Enums;

namespace Mimo.Database.Models;

[Index(nameof(AchievementId), nameof(RequiredCourseId), nameof(RequiredActivityType), nameof(RequiredActivityTotal), IsUnique = true)]
public class AchievementCriterion
{
    public int Id { get; set; }
    public int AchievementId { get; set; }
    [ForeignKey(nameof(Course))]
    public int? RequiredCourseId { get; set; }
    public ActivityType? RequiredActivityType { get; set; }
    public int? RequiredActivityTotal { get; set; }

    public Achievement Achievement { get; set; }
    public Course Course { get; set; }
}
