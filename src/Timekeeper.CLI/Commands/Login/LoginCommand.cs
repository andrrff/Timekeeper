using Spectre.Console.Cli;
using System.ComponentModel;
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
        public string? UserName { get; set; }

        [CommandOption("-p|--password <PASSWORD>")]
        [Description("The password to login.")]
        [DefaultValue("Andrrff17112001")]
        public string? Password { get; set; }
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

    public override int Execute(CommandContext context, Settings settings)
    {
        System.Console.WriteLine("LoginCommand");
        System.Console.WriteLine($"UserName: {settings.UserName}");
        System.Console.WriteLine($"Password: {settings.Password}");

        System.Console.WriteLine($"UserId: {Login(settings.UserName, settings.Password)}");

        return 0;
    }
}