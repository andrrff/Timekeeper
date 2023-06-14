using Microsoft.Extensions.DependencyInjection;
using Timekeeper.Application.Common.Interfaces;

namespace Timekeeper.CLI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IServiceProvider _serviceProvider;

    public CurrentUserService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private string CurrentId { get; set; } = string.Empty;

    public string? UserId
    {
        get
        {
            var identityService = _serviceProvider.GetRequiredService<IIdentityService>();

            if (string.IsNullOrEmpty(CurrentId))
            {
                CurrentId = identityService.CreateUserAsync("andrecastanhal@gmail.com", "@Andrrff17112001").Result.UserId;
                return CurrentId;
            }

            var username = identityService.GetUserNameAsync(CurrentId).Result ?? string.Empty;

            if (string.IsNullOrEmpty(username))
            {
                CurrentId = identityService.CreateUserAsync("andrecastanhal@gmail.com", "@Andrrff17112001").Result.UserId;
            }

            return CurrentId;
        }
    }
}
