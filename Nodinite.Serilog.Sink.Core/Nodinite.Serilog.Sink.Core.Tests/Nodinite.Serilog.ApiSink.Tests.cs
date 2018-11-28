using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using Nodinite.Serilog.Sink.Core;
using Serilog.Events;
using Serilog.Core;
using Serilog.Configuration;
using Microsoft.Extensions.Configuration;

namespace Nodinite.Serilog.Sink.Core.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReadSettingsFromAppSettingsTest()
        {
            // todo: implement moq
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Logger log = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            log.Information("Hello World");
        }
    }
}
