using Nodinite.Serilog.ApiSink.models;

namespace Nodinite.Serilog.ApiSink
{
    interface INodiniteSink
    {
        void LogMessage(NodiniteLogEvent logEvent);
    }
}
