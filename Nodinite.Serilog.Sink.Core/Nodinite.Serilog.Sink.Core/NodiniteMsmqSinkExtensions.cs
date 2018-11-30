using Nodinite.Serilog.Sink.Core.models;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;

namespace Nodinite.Serilog.Sink.Core
{
    public static class NodiniteMsmqSinkExtensions
    {
        public static LoggerConfiguration NodiniteMsmqSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  string NodiniteApiUrl, 
                  NodiniteMsmqSettings MsmqSettings,
                  NodiniteLogEventSettings Settings,
                  IFormatProvider formatProvider = null,
                  LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException("loggerConfiguration");

            return loggerConfiguration.Sink(new NodiniteMsmqSink(NodiniteApiUrl, MsmqSettings, Settings, formatProvider), restrictedToMinimumLevel);
        }
    }
}
