using Newtonsoft.Json;
using Nodinite.Serilog.Sink.Core.models;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Nodinite.Serilog.Sink.Core
{
    public class NodiniteApiSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly string _apiUrl;
        private readonly NodiniteLogEventSettings _settings;

        public NodiniteApiSink(string apiUrl, NodiniteLogEventSettings settings, IFormatProvider formatProvider)
        {
            _apiUrl = apiUrl;
            _settings = settings;
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);

            var nEvent = new NodiniteLogEvent();

            nEvent.LogAgentValueId = _settings.LogAgentValueId.HasValue ? _settings.LogAgentValueId.Value : 10000;
            nEvent.EndPointName = _settings.EndPointName;
            nEvent.EndPointUri = _settings.EndPointUri;
            nEvent.EndPointDirection = _settings.EndPointDirection.HasValue ? _settings.EndPointDirection.Value : 0;
            nEvent.EndPointTypeId = _settings.EndPointTypeId.HasValue ? _settings.EndPointTypeId.Value : 0;
            nEvent.OriginalMessageTypeName = string.IsNullOrWhiteSpace(_settings.OriginalMessageTypeName) ? "Nodinite.Serilog.LogEvent" : _settings.OriginalMessageTypeName;
            nEvent.LogDateTime = DateTimeOffset.UtcNow;
            nEvent.ProcessingUser = _settings.ProcessingUser;
            nEvent.SequenceNo = 0;
            nEvent.EventNumber = 0;
            nEvent.ApplicationInterchangeId = Guid.NewGuid().ToString();
            nEvent.LocalInterchangeId = Guid.NewGuid();
            nEvent.ProcessName = _settings.ProcessName;
            nEvent.ProcessingMachineName = _settings.ProcessingMachineName;
            nEvent.ProcessingModuleName = _settings.ProcessingModuleName;
            nEvent.ProcessingModuleType = _settings.ProcessingModuleType;

            try
            {
                nEvent.Context = new
                {
                    CorrelationId = Guid.Parse(logEvent.Properties["correlationId"].ToString())
                };
            }
            catch { }

            try
            {
                nEvent.ServiceInstanceActivityId = Guid.Parse(logEvent.Properties["serviceInstanceActivityId"].ToString());
            }
            catch
            {
                nEvent.ServiceInstanceActivityId = Guid.NewGuid();
            }

            nEvent.ProcessingTime = 0;
            nEvent.LogText = message;

            switch (logEvent.Level)
            {
                case LogEventLevel.Error:
                    nEvent.LogStatus = -1;
                    break;
                case LogEventLevel.Fatal:
                    nEvent.LogStatus = -2;
                    break;
                case LogEventLevel.Warning:
                    nEvent.LogStatus = 1;
                    break;
                case LogEventLevel.Debug:
                case LogEventLevel.Information:
                case LogEventLevel.Verbose:
                default:
                    nEvent.LogStatus = 0;
                    break;
            }

            LogMessageToNodinite(nEvent);
        }

        private void LogMessageToNodinite(NodiniteLogEvent logEvent)
        {
            using (var client = new HttpClient())
            {
                var uri = _apiUrl + "LogEvent/LogEvent";

                HttpResponseMessage response;


                byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(logEvent));

                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var responseTask = client.PostAsync(uri, content);
                    responseTask.Wait();
                    response = responseTask.Result;
                }
            }
        }
    }
}
