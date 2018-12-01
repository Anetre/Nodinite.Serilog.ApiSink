using Nodinite.Serilog.Sink.Core.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nodinite.Serilog.Sink.Core
{
    interface INodiniteSink
    {
        void LogMessage(NodiniteLogEvent logEvent);
    }
}
