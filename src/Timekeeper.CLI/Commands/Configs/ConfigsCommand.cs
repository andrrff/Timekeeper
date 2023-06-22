using MediatR;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Timekeeper.Domain.ValueObjects.Configs;
using Microsoft.Extensions.DependencyInjection;
using Timekeeper.Application.UserConfigs.Commands.CreateUserConfig;

namespace Timekeeper.CLI.Commands.Configs;

[Description("AzDevOps commands.")]
public sealed class ConfigsCommand : Command<ConfigsCommand.Settings>
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
    }

    public ConfigsCommand(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _mediator        = _serviceProvider.GetService<ISender>();
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        _mediator?.Send(new CreateUserConfigsCommand
        {
            AzDevOpsParameters = new AzDevOpsParameters(settings.ApiKey, settings.Instance, settings.Project, settings.Repository)
        });

        return 0;
    }
}