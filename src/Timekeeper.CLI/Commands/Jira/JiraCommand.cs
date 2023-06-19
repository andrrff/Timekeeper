using Spectre.Console.Cli;
using System.ComponentModel;
using Timekeeper.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Timekeeper.Infrastructure.ExternalServices.Jira;

namespace Timekeeper.CLI.Commands.Jira;

[Description("Jira commands.")]
public sealed class JiraCommand : Command<JiraCommand.Settings>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICurrentUserService _currentUserService;

    public class Settings : CommandSettings
    {
        [CommandOption("-u|--username <USERNAME>")]
        [Description("The username to login.")]
        [DefaultValue("atendimentorci@nexergroup.com")]
        public string UserName { get; set; } = string.Empty;

        [CommandOption("-k|--api-key <KEY>")]
        [Description("API Key to basic auth.")]
        [DefaultValue("ATATT3xFfGF0adpgDI2XoU2L2v4ABTyi26d6pI4APYEXWCV4e67ds84Lwicfs4xaPj-LzoDnmXI4QseOvbfU8LK8yVnvWRQALooYjDNGic-VZuz7-DYJP8mVvWSzUO2EKZjCexfkA7jyLCMvA_U7RKwa76hF4YyixK9te9hrSfHFvgabMQN45Go=1E2BE929")]
        public string ApiKey { get; set; } = string.Empty;

        [CommandOption("-i|--instance <INSTANCE>")]
        [Description("https://[Instance-Name].atlassian.net/")]
        [DefaultValue("gruporcibrasil")]
        public string Instance { get; set; } = string.Empty;

        [CommandOption("-e|--endpoint <ENDPOINT>")]
        [Description("Name of endpoint from API Jira (Example: 'Search').")]
        [DefaultValue("search")]
        public string Endpoint { get; set; } = string.Empty;

        [CommandOption("-v|--value <VALUE>")]
        [Description("Value to use in query for endpoint from API Jira.")]
        public string Value { get; set; } = string.Empty;
    }

    public JiraCommand(IServiceProvider serviceProvider, ICurrentUserService currentUserService)
    {
        _serviceProvider = serviceProvider;
        _currentUserService = currentUserService;
    }

    public async Task<string> GetSearchAsync(string userName, string password, string instance, string value)
    {
        var jiraClient = new JiraClient(userName, password, instance);
        
        return await jiraClient.GetSearchAsync(value);
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        System.Console.WriteLine($"Welcome, {settings.UserName}");
        System.Console.WriteLine($"Result from Jira: {GetSearchAsync(settings.UserName, settings.ApiKey, settings.Instance, settings.Value).Result}");

        return 0;
    }
}