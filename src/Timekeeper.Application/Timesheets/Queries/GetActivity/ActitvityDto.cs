using Timekeeper.Domain.Entities;
using Timekeeper.Application.Common.Mappings;
using Timekeeper.Domain.ValueObjects.Tasks;

namespace Timekeeper.Application.Timesheets.Queries.GetActivity;

public class ActitvityDto : IMapFrom<Activity>
{
    public int TimesheetId { get; set; }

    public string? Note { get; set; }

    public TaskItem TaskItem { get; set; } = default!;
}