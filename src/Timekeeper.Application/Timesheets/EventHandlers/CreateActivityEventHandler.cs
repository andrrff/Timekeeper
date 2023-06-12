using MediatR;
using Microsoft.Extensions.Logging;
using Timekeeper.Domain.Events.Activities;

namespace Timekeeper.Application.Timesheets.EventHandlers;

public class CreateActivityEventHandler : INotificationHandler<ActivityCreatedEvent>
{
    private readonly ILogger<ActivityCreatedEvent> _logger;

    public CreateActivityEventHandler(ILogger<ActivityCreatedEvent> logger)
    {
        _logger = logger;
    }

    public Task Handle(ActivityCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timekeeper Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}