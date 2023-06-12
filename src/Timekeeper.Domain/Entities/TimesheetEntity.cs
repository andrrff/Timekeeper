using Timekeeper.Domain.Common;

namespace Timekeeper.Domain.Entities;

public class Timesheet : BaseAuditableEntity
{
    public int Month { get; set; }

    public int Year { get; set; }

    public int TotalHours { get; set; }

    public IEnumerable<Activity> Activity { get; set; } = new List<Activity>();
}