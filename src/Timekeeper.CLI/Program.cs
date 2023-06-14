using Timekeeper.CLI.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        var appService = host.Services.GetRequiredService<IAppService>();

        appService.Run();
        await host.RunAsync();
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