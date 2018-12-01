using Nodinite.Serilog.Sink.models;

namespace Nodinite.Serilog.Sink
{
    interface INodiniteSink
    {
        void LogMessage(NodiniteLogEvent logEvent);
    }
}
