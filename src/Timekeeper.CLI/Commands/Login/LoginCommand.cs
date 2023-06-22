using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Timekeeper.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Timekeeper.CLI.Commands.Login;

[Description("Login to the application.")]
public sealed class LoginCommand : Command<LoginCommand.Settings>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICurrentUserService _currentUserService;

    public class Settings : CommandSettings
    {
        [CommandOption("-u|--username <USERNAME>")]
        [Description("The username to login.")]
        [DefaultValue("andrecastanhl@gmail.com")]
        public string UserName { get; set; } = string.Empty;

        [CommandOption("-p|--password <PASSWORD>")]
        [Description("The password to login.")]
        [DefaultValue("Andrrff17112001")]
        public string Password { get; set; } = string.Empty;
    }

    public LoginCommand(IServiceProvider serviceProvider, ICurrentUserService currentUserService)
    {
        _serviceProvider    = serviceProvider;
        _currentUserService = currentUserService;
    }

    public string Login(string userName, string password)
    {
        var identityService = _serviceProvider.GetRequiredService<IIdentityService>();
        _currentUserService.SetUserId(identityService.AuthenticateAsync(userName, password).Result.UserId);

        return _currentUserService.UserId ?? string.Empty;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        System.Console.WriteLine($"Welcome, {settings.UserName}");
        System.Console.WriteLine($"UserId: {Login(settings.UserName, settings.Password)}");

        return 0;
    }
}