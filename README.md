[![Nodinite Logo](https://www.nodinite.com/wp-content/uploads/2018/10/Nodinite_logo_payoff2line_w195.png)](https://nodinite.com)

# Nodinite.Serilog.Sink.Core

[![Build status](https://ci.appveyor.com/api/projects/status/a8r7xt17s7x0vxca?svg=true)](https://ci.appveyor.com/project/syron/nodinite-serilog-sink-core)

A Serilog sink that writes log events to **Nodinite**. This project is built with .NET Core 2.0.

The current version supports logging **Nodinite** Log Events using

* Log API

Upcoming versions support logging **Nodinite** Log Events using

* MSMQ 
* Azure Service Bus

Events that are logged to MSMQ and Azure Service Bus can then be picked up and logged to **Nodinite** using our [Pickup Events Service](https://documentation.nodinite.com/Documentation/LoggingAndMonitoring/Pickup%20LogEvents%20Service?doc=/Overview).

## Get Started

## Configuration

### Appsettings.json

`{
  "Serilog": {
    "Using": [ "Nodinite.Serilog.Sink.Core" ],
    "WriteTo": [
      {
        "Name": "NodiniteApiSink",
        "Args": {
          "NodiniteApiUrl": "",
          "settings": {
            "LogAgentValueId": 503,
            "EndPointName": "Nodinite.Serilog.Sink.Tests",
            "EndPointUri": "Nodinite.Serilog.Sink.Tests.Serilog",
            "EndPointDirection": 0,
            "EndPointTypeId": 0,
            "OriginalMessageTypeName": "Serilog.LogEvent",
            "ProcessingUser": "NODINITE",
            "ProcessName": "Nodinite.Serilog.Sink.Tests",
            "ProcessingMachineName": "NODINITE-DEV",
            "ProcessingModuleName": "DOTNETCORE.TESTS",
            "ProcessingModuleType": "DOTNETCORE.TESTPROJECT"
          }
        }
      }
    ]
  }
}`