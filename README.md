[![Nodinite Logo](https://www.nodinite.com/wp-content/uploads/2018/10/Nodinite_logo_payoff2line_w195.png)](https://nodinite.com)

# Nodinite.Serilog.Sink.Core

[![Build status](https://ci.appveyor.com/api/projects/status/a8r7xt17s7x0vxca?svg=true)](https://ci.appveyor.com/project/syron/nodinite-serilog-sink-core)
[![Nuget](https://img.shields.io/badge/nuget-1.0.2-orange.svg)](https://www.nuget.org/packages/Nodinite.Serilog.Sink.Core)

A [Serilog](https://www.nuget.org/packages/Serilog/2.7.2-dev-01033) sink that writes log events to [**Nodinite**](https://nodinite.com). This project is built with .NET Core 2.0.

The current version supports logging [**Nodinite**](https://nodinite.com) Log Events using

* [Log API](https://documentation.nodinite.com/Documentation/CoreServices?doc=/Log%20API/Overview)

Upcoming versions support logging [**Nodinite**](https://nodinite.com) Log Events using

* MSMQ 
* Azure Service Bus
* RabbitMQ
* ActiveMQ

Events that are logged to one of the messaging systems above can then be picked up and logged to [**Nodinite**](https://nodinite.com) using our [Pickup Events Service](https://documentation.nodinite.com/Documentation/LoggingAndMonitoring/Pickup%20LogEvents%20Service?doc=/Overview).

## Get Started

### Install Nodinite.Serilog.Sink.Core Nuget Package

Start by installing the NuGet package [Nodinite.Serilog.Sink.Core](https://www.nuget.org/packages/Nodinite.Serilog.Sink.Core/).

```
Install-Package Nodinite.Serilog.Sink.Core
```

### Configuration

#### Using code

```csharp
var nodiniteApiUrl = "{Your Nodinite API Url";
var settings = new NodiniteLogEventSettings()
{
    LogAgentValueId = 503,
    EndPointDirection = 0,
    EndPointTypeId = 0,
    EndPointUri = "Nodinite.Serilog.Sink.Tests.Serilog",
    EndPointName = "Nodinite.Serilog.Sink.Tests",
    OriginalMessageTypeName = "Serilog.LogEvent",
    ProcessingUser = "NODINITE",
    ProcessName = "Nodinite.Serilog.Sink.Tests",
    ProcessingMachineName = "NODINITE-DEV",
    ProcessingModuleName = "DOTNETCORE.TESTS",
    ProcessingModuleType = "DOTNETCORE.TESTPROJECT"
};

Logger log = new LoggerConfiguration()
    .WriteTo.NodiniteApiSink(nodiniteApiUrl, settings)
    .CreateLogger();
```

#### Using Appsettings.json (Preferred)

The following nuget packages need to be installed

* [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/2.2.0-preview3-35497)
* [Microsoft.Extensions.Configuration.Json](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json/2.2.0-preview3-35497)
* Nodinite.Serilog.Sink.Core
* [Serilog.Settings.Configuration](https://www.nuget.org/packages/Serilog.Settings.Configuration/)

Using the following code to initialize the logger in your application:

```csharp
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Logger log = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
```

And putting the following into your appsettings.json:

```json
{
  "Serilog": {
    "Using": [ "Nodinite.Serilog.Sink.Core" ],
    "WriteTo": [
      {
        "Name": "NodiniteApiSink",
        "Args": {
          "NodiniteApiUrl": "",
          "Settings": {
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
}
```