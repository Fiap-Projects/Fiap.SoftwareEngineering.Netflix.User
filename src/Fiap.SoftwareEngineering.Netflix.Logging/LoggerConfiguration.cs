using Fiap.SoftwareEngineering.Netflix.Logging.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace Fiap.SoftwareEngineering.Netflix.Logging
{
    public class LoggerConfiguration : ILoggerConfiguration
    {
        public ILogger SetUp(string environmentVariable)
        {
            var environment = Environment.GetEnvironmentVariable(environmentVariable);

            var logConfig = new Serilog.LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName();

            if (environment == EnvironmentName.Development)
                logConfig.WriteTo.Console(theme: AnsiConsoleTheme.Code);
            else
                logConfig.WriteTo.Console(new ElasticsearchJsonFormatter());

            Log.Logger = logConfig.CreateLogger();
            return Log.Logger;
        }

        public static ILogger CreateLogger(string environmentVariable) =>
            new LoggerConfiguration().SetUp(environmentVariable);
    }
}
