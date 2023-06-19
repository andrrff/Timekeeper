using MediatR;
using Timekeeper.Application.Common.Interfaces;
using Timekeeper.Domain.ValueObjects.Configs;

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
        var entity = new UserConfigs
        {
            JiraParameters    = request.JiraParameters,
            AzDevOpsParameters = request.AzDevOpsParameters
        };

        _context.UserConfigs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}