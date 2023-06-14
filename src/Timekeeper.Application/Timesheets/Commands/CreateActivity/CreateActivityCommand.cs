using MediatR;
using Timekeeper.Domain.Entities;
using Timekeeper.Domain.Events.Activities;
using Timekeeper.Domain.ValueObjects.Tasks;
using Timekeeper.Application.Common.Interfaces;

namespace Timekeeper.Application.Timesheets.Commands.CreateActivity;

public class CreateActivityCommand : IRequest<Guid>
{
    public Guid TimesheetId { get; set; }

    public string? Note { get; set; }

    public TaskItem TaskItem { get; set; } = null!;
}

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateActivityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var entity = new Activity
        {
            TimesheetId = request.TimesheetId,
            Note        = request.Note,
            TaskItem    = request.TaskItem
        };

        entity.AddDomainEvent(new ActivityCreatedEvent(entity));

        _context.Activities.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.TimesheetId;
    }
}