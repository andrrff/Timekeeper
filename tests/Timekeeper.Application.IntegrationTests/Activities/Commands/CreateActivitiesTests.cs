using FluentAssertions;
using Timekeeper.Application.Common.Exceptions;
using Timekeeper.Application.Timesheets.Commands.CreateActivity;

namespace Timekeeper.Application.IntegrationTests.Activities.Commands;

using static Testing;

public class CreateActivitiesTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateActivityCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}