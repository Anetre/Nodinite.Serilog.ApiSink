using System;
using System.Collections.Generic;
using System.Text;

namespace Nodinite.Serilog.Sink.Core.models
{
    public class NodiniteLogEvent
    {
        public int LogAgentValueId { get; set; }
        public string EndPointName { get; set; }
        public string EndPointUri { get; set; }
        public int EndPointDirection { get; set; }
        public int EndPointTypeId { get; set; }
        public string OriginalMessageTypeName { get; set; }
        public DateTimeOffset LogDateTime { get; set; }
        public string ProcessingUser { get; set; }
        public int SequenceNo { get; set; }
        public int EventNumber { get; set; }
        public string LogText { get; set; }
        public string ApplicationInterchangeId { get; set; }
        public Guid LocalInterchangeId { get; set; }
        public int LogStatus { get; set; }
        public string ProcessName { get; set; }
        public string ProcessingMachineName { get; set; }
        public string ProcessingModuleName { get; set; }
        public string ProcessingModuleType { get; set; }
        public Guid ServiceInstanceActivityId { get; set; }
        public int ProcessingTime { get; set; }
        public string Body { get; set; }
        public object Context { get; set; }
    }
}
