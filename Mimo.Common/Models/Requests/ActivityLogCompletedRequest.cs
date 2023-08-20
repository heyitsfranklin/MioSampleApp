using Mimo.Common.Enums;

namespace Mimo.Common.Models.Requests;

public class ActivityLogCompletedRequest
{
    /// <summary>
    /// The Id of the course, chapter, or lesson to mark as completed
    /// </summary>
    public int ActivityId { get; set; }
    /// <summary>
    /// The type of activity that is being updated
    /// </summary>
    public ActivityType ActivityType { get; set; }
    /// <summary>
    /// The date the activity was completed
    /// </summary>
    public DateTimeOffset CompletedDate { get; set; }
}