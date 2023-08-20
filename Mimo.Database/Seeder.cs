using Microsoft.EntityFrameworkCore;
using Mimo.Common.Enums;
using Mimo.Database.Models;

namespace Mimo.Database;

internal static class Seeder
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        #region users
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                CreatedDate = DateTimeOffset.Now
            });
        #endregion
        
        #region courses
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Title = "Learn C#",
                Description = "A course on C#"
            },
            new Course
            {
                Id = 2,
                Title = "Learn Javascript",
                Description = "A course on Javascript"
            });
        #endregion
        
        #region chapters
        modelBuilder.Entity<Chapter>().HasData(
            new Chapter
            {
                Id = 1,
                Title = "Chapter 1",
                CourseId = 1,
                DisplayOrder = 1
            },
            new Chapter
            {
                Id = 2,
                Title = "Chapter 2",
                CourseId = 1,
                DisplayOrder = 2
            },
            new Chapter
            {
                Id = 3,
                Title = "Chapter 3",
                CourseId = 1,
                DisplayOrder = 3
            },
            new Chapter
            {
                Id = 4,
                Title = "Chapter 1",
                CourseId = 2,
                DisplayOrder = 1
            });
        #endregion

        #region lessons
        modelBuilder.Entity<Lesson>().HasData(
            new Lesson
            {
                Id = 1,
                Title = "Lesson 1",
                ChapterId = 1,
                DisplayOrder = 1
            },
            new Lesson
            {
                Id = 2,
                Title = "Lesson 1",
                ChapterId = 2,
                DisplayOrder = 1
            },
            new Lesson
            {
                Id = 3,
                Title = "Lesson 2",
                ChapterId = 2,
                DisplayOrder = 2
            },
            new Lesson
            {
                Id = 4,
                Title = "Lesson 1",
                ChapterId = 4,
                DisplayOrder = 1
            },
            new Lesson
            {
                Id = 5,
                Title = "Lesson 2",
                ChapterId = 4,
                DisplayOrder = 2
            });
        #endregion
        
        #region achievements
        modelBuilder.Entity<Achievement>().HasData(
            new Achievement
            {
                Id = 1,
                Title = "Complete C# course"
            },
            new Achievement
            {
                Id = 2,
                Title = "Complete three chapters"
            },
            new Achievement
            {
                Id = 3,
                Title = "Complete 5 lessons"
            });
        #endregion
        
        #region criteria
        modelBuilder.Entity<AchievementCriterion>().HasData(
            new AchievementCriterion
            {
                Id = 1,
                AchievementId = 2,
                RequiredActivityType = ActivityType.Chapter,
                RequiredActivityTotal = 3
            },
            new AchievementCriterion
            {
                Id = 2,
                AchievementId = 3,
                RequiredActivityType = ActivityType.Lesson,
                RequiredActivityTotal = 5
            },
            new AchievementCriterion
            {
                Id = 3,
                AchievementId = 1,
                RequiredCourseId = 1
            });
        #endregion

        #region achievement log
        modelBuilder.Entity<AchievementLog>().HasData(
            new AchievementLog()
            {
                AchievementId = 1,
                UserId = 1
            },
            new AchievementLog()
            {
                AchievementId = 2,
                UserId = 1
            });
        #endregion

        #region activity log
        modelBuilder.Entity<ActivityLog>().HasData(
            new ActivityLog
            {
                Id = 1,
                CourseId = 1,
                UserId = 1,
                StartDate = DateTimeOffset.Now,
                CompletedDate = DateTimeOffset.Now
            },
            new ActivityLog
            {
                Id = 2,
                ChapterId = 1,
                UserId = 1,
                StartDate = DateTimeOffset.Now,
                CompletedDate = DateTimeOffset.Now
            },
            new ActivityLog
            {
                Id = 3,
                ChapterId = 2,
                UserId = 1,
                StartDate = DateTimeOffset.Now,
                CompletedDate = DateTimeOffset.Now
            },
            new ActivityLog
            {
                Id = 4,
                ChapterId = 3,
                UserId = 1,
                StartDate = DateTimeOffset.Now,
                CompletedDate = DateTimeOffset.Now
            },
            new ActivityLog
            {
                Id = 5,
                CourseId = 2,
                UserId = 1,
                StartDate = DateTimeOffset.Now,
                CompletedDate = null
            },
            new ActivityLog
            {
                Id = 6,
                LessonId = 4,
                UserId = 1,
                StartDate = DateTimeOffset.Now,
                CompletedDate = DateTimeOffset.Now
            },
            new ActivityLog
            {
                Id = 7,
                LessonId = 5,
                UserId = 1,
                StartDate = DateTimeOffset.Now,
                CompletedDate = DateTimeOffset.Now
            });
        #endregion
    }
}