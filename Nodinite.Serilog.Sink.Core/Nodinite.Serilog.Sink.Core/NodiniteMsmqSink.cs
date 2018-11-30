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
    public class NodiniteMsmqSink : ILogEventSink, INodiniteSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly string _apiUrl;
        private readonly NodiniteLogEventSettings _settings;
        private readonly NodiniteMsmqSettings _msmqSettings;

        public NodiniteMsmqSink(string apiUrl, NodiniteMsmqSettings msmqSettings, NodiniteLogEventSettings settings, IFormatProvider formatProvider)
        {
            _apiUrl = apiUrl;
            _msmqSettings = msmqSettings;
            _settings = settings;
            _formatProvider = formatProvider;

            // validate settings
            if (!_settings.LogAgentValueId.HasValue)
                throw new ArgumentNullException("LogAgentValueId must not be null");
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);

            var nEvent = new NodiniteLogEvent(message, logEvent, _settings);

            LogMessage(nEvent);
        }

        public void LogMessage(NodiniteLogEvent logEvent)
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
