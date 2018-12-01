﻿using Nodinite.Serilog.Sink.models;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;

namespace Nodinite.Serilog.Sink
{
    public static class NodiniteApiSinkExtensions
    {
        public static LoggerConfiguration NodiniteApiSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  string NodiniteApiUrl, 
                  NodiniteLogEventSettings Settings,
                  IFormatProvider formatProvider = null,
                  LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException("loggerConfiguration");

            return loggerConfiguration.Sink(new NodiniteApiSink(NodiniteApiUrl, Settings, formatProvider), restrictedToMinimumLevel);
        }
    }
}
