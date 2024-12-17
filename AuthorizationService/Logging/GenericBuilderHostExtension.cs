//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using Microsoft.Playwright.Common.Utilities;
using Serilog;
using Serilog.Context;
using Serilog.Formatting.Compact;
using Serilog.Sinks.AzureDataExplorer.Extensions;

namespace AuthorizationService.Logging
{
    public static class GenericBuilderHostExtension
    {

        public static IHostBuilder UsePlaywrightLogger(this IHostBuilder builder)
        {
            // https://andrewlock.net/adding-serilog-to-the-asp-net-core-generic-host/
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .ConfigureServices((hostContext, services) =>
                {
                    var serilogConfig = new LoggerConfiguration();
                    Serilog.Debugging.SelfLog.Enable(Console.Error);
                    serilogConfig = serilogConfig.ReadFrom.Configuration(hostContext.Configuration);
                    //serilogConfig.Enrich.WithCorrelationIdHeader();//using default header name  "x-correlation-id"
                    //serilogConfig.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss:ms} {CorrelationId} {Level:u3}] {Message:lj}{NewLine}{Exception}");

                    serilogConfig = serilogConfig.WriteTo.Console(new CompactJsonFormatter());
                    /*serilogConfig = serilogConfig.WriteTo.AzureDataExplorerSink(new Serilog.Sinks.AzureDataExplorer.AzureDataExplorerSinkOptions
                    {
                        IngestionEndpointUri = "https://ingest-serilogsinktest.eastus.kusto.windows.net",
                        DatabaseName = "serilogdb",
                        TableName = "serilogtable",
                        FlushImmediately = true,
                    });*/

                    serilogConfig.Enrich.FromGlobalLogContext();
                    GlobalLogContext.PushProperty(Constants.Environment, "Prod");
                    GlobalLogContext.PushProperty(Constants.Region, "eastus");
                    GlobalLogContext.PushProperty(Constants.Instance, "88071");
                    var logger = serilogConfig.CreateLogger();
                    Log.Logger = logger;


                    services.AddSingleton(Log.Logger);
                })
                .UseSerilog();
        }
    }
}
