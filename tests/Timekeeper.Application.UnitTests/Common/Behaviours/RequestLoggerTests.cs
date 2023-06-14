using Moq;
using Microsoft.Extensions.Logging;
using Timekeeper.Application.Common.Behaviours;
using Timekeeper.Application.Common.Interfaces;
using Timekeeper.Application.Timesheets.Commands.CreateActivity;
using Timekeeper.Domain.ValueObjects.Tasks;
using Timekeeper.Domain.Enums;

namespace Timekeeper.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateActivityCommand>> _logger = default!;
    private Mock<ICurrentUserService> _currentUserService = default!;
    private Mock<IIdentityService> _identityService = default!;

    [SetUp]
    public void Setup()
    {
        _logger             = new Mock<ILogger<CreateActivityCommand>>();
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService    = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateActivityCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateActivityCommand 
        { 
            TimesheetId = new Guid(),
            TaskItem    = new TaskItem("MC2-1010", "title", "link.com", OriginType.Jira, TaskType.Development, 40, 32, DateTime.Now, DateTime.Now),
            Note        = "note"
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateActivityCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateActivityCommand
        {
            TimesheetId = new Guid(),
            TaskItem    = new TaskItem("MC2-1010", "title", "link.com", OriginType.Jira, TaskType.Development, 40, 32, DateTime.Now, DateTime.Now),
            Note        = "note"
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}