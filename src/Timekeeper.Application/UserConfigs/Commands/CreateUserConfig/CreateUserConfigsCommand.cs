using MediatR;
using Timekeeper.Domain.Entities;
using Timekeeper.Domain.Events.UserConfigs;
using Timekeeper.Domain.ValueObjects.Configs;
using Timekeeper.Application.Common.Interfaces;

namespace Timekeeper.Application.UserConfigs.Commands.CreateUserConfig;

public class CreateUserConfigsCommand : IRequest
{
    public JiraParameters? JiraParameters { get; set; }
    
    public AzDevOpsParameters? AzDevOpsParameters { get; set; }
}

public class CreateUserConfigsCommandHandler : IRequestHandler<CreateUserConfigsCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateUserConfigsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateUserConfigsCommand request, CancellationToken cancellationToken)
    {
        var entity = new UserConfig
        {
            JiraParameters     = request.JiraParameters,
            AzDevOpsParameters = request.AzDevOpsParameters
        };

        entity.AddDomainEvent(new UserConfigCreatedEvent(entity));

        _context.UserConfigs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}