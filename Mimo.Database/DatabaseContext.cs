using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Mimo.Database.Models;

namespace Mimo.Database;

public class DatabaseContext : DbContext
{
    private readonly IHostEnvironment _environment;

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<AchievementCriterion> AchievementCriteria { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<AchievementLog> AchievementLogs { get; set; }
    
    public DatabaseContext(DbContextOptions options, IHostEnvironment environment) : base(options)
    {
        _environment = environment;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder
            .Entity<ActivityLog>()
            .ToTable(a => 
                a.HasCheckConstraint("ckActivityLog_RequireCourseChapterOrLessonId", $"CourseId is not null or ChapterId is not null or LessonId is not null"));
        
        modelBuilder
            .Entity<AchievementCriterion>()
            .ToTable(a => 
                a.HasCheckConstraint("ckAchievementCriterion_RequireCourseIdOrAggregateSum", $"RequiredCourseId is not null or ({nameof(AchievementCriterion.RequiredActivityType)} is not null and {nameof(AchievementCriterion.RequiredActivityTotal)} is not null)"));
        
        if (_environment.IsDevelopment())
            modelBuilder.Seed();
    }
}
