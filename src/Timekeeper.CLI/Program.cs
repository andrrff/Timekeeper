using Spectre.Console.Cli;
using Timekeeper.CLI.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Timekeeper.CLI.Commands.Login;
using ZymLabs.NSwag.FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Timekeeper.Application.Common.Interfaces;

namespace Timekeeper.CLI;

public class Program
{
    static async Task Main(string[] args)
    {
        var host       = CreateHostBuilder(args).Build();
        var service    = CreateService();
        var registrar  = new TypeRegistrar(service);
        var appService = host.Services.GetRequiredService<IAppService>();
        var appCli     = new CommandApp(registrar);

        appCli.Configure(config =>
        {
            config.SetApplicationName("Timekeeper.CLI");
            config.AddCommand<LoginCommand>("login");
            config.AddCommand<RegisterCommand>("register");
        });

        await appCli.RunAsync(args);
        appService.Run();
        await host.StopAsync();
    }

    static ServiceCollection CreateService()
    {
        var services      = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<IAppService, AppService>();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped(provider =>
        {
            var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            var loggerFactory   = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        return services;
    }


    static IHostBuilder CreateHostBuilder(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IConfiguration>(configuration);
                services.AddSingleton<IAppService, AppService>();
                services.AddSingleton<ICommandApp, CommandApp>();
                services.AddApplicationServices();
                services.AddInfrastructureServices(hostContext.Configuration);
                services.AddScoped<ICurrentUserService, CurrentUserService>();
                services.AddScoped(provider =>
                {
                    var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
                    var loggerFactory   = provider.GetService<ILoggerFactory>();

                    return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
                });
            });
    }
}