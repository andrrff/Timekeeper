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

    public void Register(string userName, string password)
    {
        var identityService = _serviceProvider.GetRequiredService<IIdentityService>();

        CurrentId = identityService.CreateUserAsync(userName, password).Result.UserId;
    }

    public string Login(string userName, string password)
    {
        var identityService = _serviceProvider.GetRequiredService<IIdentityService>();

        CurrentId = identityService.AuthenticateAsync(userName, password).Result.UserId;

        return CurrentId;
    }

    public void Logout()
    {
        CurrentId = string.Empty;
    }

    public void SetUserId(string userId)
    {
        CurrentId = userId;
    }

    public string? UserId => CurrentId;
}
