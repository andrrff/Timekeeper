using MediatR;
using Timekeeper.Domain.Enums;
using Timekeeper.Domain.ValueObjects.Tasks;
using Timekeeper.Application.Timesheets.Commands.CreateActivity;

namespace Timekeeper.CLI.Services;

public class AppService : IAppService
{
    private readonly ISender? _mediator;

    public AppService(ISender mediator)
    {
        _mediator = mediator;
    }

    public void Run()
    {
        _mediator?.Send(new CreateActivityCommand
        {
            TimesheetId = Guid.NewGuid(),
            TaskItem    = new TaskItem("MC2-1010", "title", "https://www.example.com/", OriginType.Jira, TaskType.Development, 40, 32, DateTime.Now, DateTime.Now),
            Note    	= "note"
        });
    }
}