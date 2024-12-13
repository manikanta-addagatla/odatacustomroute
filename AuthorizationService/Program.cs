//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using AuthorizationService.Logging;
using Serilog;

namespace Microsoft.Playwright.Services.Authorization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // The initial "bootstrap" logger is able to log errors during start-up. It's completely replaced by the
            // logger configured in `UseSerilog()` below, once configuration and dependency-injection have both been
            // set up successfully.
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();
            Log.Information("Starting up Authorization Service");
            CreateHostBuilder(args).UsePlaywrightLogger().Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((context, config) =>
                 {

                     var environment = context.HostingEnvironment.EnvironmentName.ToLower();

                     var builtConfig = config
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                         .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                         .AddEnvironmentVariables()
                         .Build();

                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}