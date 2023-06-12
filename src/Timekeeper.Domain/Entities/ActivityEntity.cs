using Timekeeper.Domain.Common;
using Timekeeper.Domain.ValueObjects.Tasks;

namespace Timekeeper.Domain.Entities;

public class Activity : BaseAuditableEntity
{
    public int TimesheetId { get; set; }

    public string? Note { get; set; }

    public TaskItem TaskItem { get; set; } = default!;

    public Timesheet Timesheet { get; set; } = default!;
}