using MediatR;
using Timekeeper.Domain.ValueObjects.Configs;

namespace Timekeeper.Application.UserConfigs.Commands.UpdateUserConfig;

public record UpdateUserConfigCommand : IRequest
{
    public JiraParameters? JiraParameters { get; set; }

    public AzDevOpsParameters? AzDevOpsParameters { get; set; }
}

public class UpdateUserConfigCommandHandler : IRequestHandler<UpdateUserConfigCommand>
{
    public Task Handle(UpdateUserConfigCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}