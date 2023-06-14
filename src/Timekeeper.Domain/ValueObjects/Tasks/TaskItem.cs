using Timekeeper.Domain.Common;
using Timekeeper.Domain.Enums;

namespace Timekeeper.Domain.ValueObjects.Tasks;

public class TaskItem : ValueObject
{
    public string Id { get; private set; } = default!;
    
    public string Title { get; private set; } = default!;
    
    public string Link { get; private set; } = default!;

    public string? Description { get; private set; }

    public int TimeToBeSpent { get; private set; }

    public int TimeSpent { get; private set; }

    public OriginType Origin { get; private set; } = default!;

    public TaskType Type { get; private set; } = default!;

    public DateTime? StartDate { get; private set; }

    public DateTime? DueDate { get; private set; }

    public TaskItem(string id, string title, string link, OriginType origin, TaskType type, int timeToBeSpent, int timeSpent, DateTime? startDate, DateTime? dueDate, string? description = null)
    {
        Id             = id;
        Title          = title;
        Link           = link;
        Origin         = origin;
        Type           = type;
        TimeToBeSpent  = timeToBeSpent;
        TimeSpent      = timeSpent;
        StartDate      = startDate;
        DueDate        = dueDate;
        Description    = description;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Title;
        yield return Link;
        yield return Origin;
        yield return Type;
        yield return TimeToBeSpent;
        yield return TimeSpent;

        if (StartDate is not null)
            yield return StartDate;

        if (DueDate is not null)
            yield return DueDate;

        if (Description is not null)
            yield return Description;
    }
}