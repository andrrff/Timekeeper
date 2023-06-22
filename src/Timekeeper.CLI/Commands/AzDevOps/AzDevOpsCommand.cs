using MediatR;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Timekeeper.Infrastructure.ExternalServices.AzDevOps;

namespace Timekeeper.CLI.Commands.AzDevOps;

[Description("AzDevOps commands.")]
public sealed class AzDevOpsCommand : Command<AzDevOpsCommand.Settings>
{
    private readonly ISender? _mediator;
    private readonly IServiceProvider _serviceProvider;
    
    public class Settings : CommandSettings
    {
        [CommandOption("-k|--api-key <KEY>")]
        [Description("PAT Key to basic auth.")]
        [DefaultValue("jmrdp5divp5dohzfxra6k77s4rzflxhovhuhi6aifrdnbutolz4q")]
        public string ApiKey { get; set; } = string.Empty;

        [CommandOption("-i|--instance <INSTANCE>")]
        [Description("https://dev.azure.com/[Organization]/")]
        [DefaultValue("loopsterbr")]
        public string Instance { get; set; } = string.Empty;

        [CommandOption("-p|--project <PROJECT>")]
        [Description("https://dev.azure.com/[Organization]/[Project]/")]
        [DefaultValue("LoopsterBR - Easy Fleet")]
        public string Project { get; set; } = string.Empty;

        [CommandOption("-r|--repository <REPOSITORY>")]
        [Description("https://dev.azure.com/[Organization]/[Project]/_git/[Repository]/")]
        [DefaultValue("Loopster - Azure Functions")]
        public string Repository { get; set; } = string.Empty;

        [CommandOption("-v|--value <VALUE>")]
        [Description("Value to use in query for endpoint from API Jira.")]
        [DefaultValue("3685")]
        public string Value { get; set; } = string.Empty;
    }

    public AzDevOpsCommand(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _mediator        = _serviceProvider.GetService<ISender>();
    }

    public static async Task GetWorkItemAsync(string instance, string project, string apiKey, string value)
    {
        var azDevOpsClient = new AzDevOpsClient(instance, project, apiKey);
        
        await azDevOpsClient.GetWorkItemAsync(int.Parse(value));
    }

    public static async Task GetCommitsAsync(string instance, string project, string repository, string apiKey)
    {
        var azDevOpsClient = new AzDevOpsClient(instance, project, apiKey);
        
        await azDevOpsClient.GetCommitsAsync(repository);
    }

    public static async Task GetChangesetsAsync(string instance, string project, string apiKey)
    {
        var azDevOpsClient = new AzDevOpsClient(instance, project, apiKey);
        
        await azDevOpsClient.GetChangesetsAsync();
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        // GetWorkItemAsync(settings.Instance, settings.Project, settings.ApiKey, settings.Value).Wait();
        // GetCommitsAsync(settings.Instance, settings.Project, settings.Repository, settings.ApiKey).Wait();
        GetChangesetsAsync(settings.Instance, settings.Project, settings.ApiKey).Wait();

        return 0;
    }
}