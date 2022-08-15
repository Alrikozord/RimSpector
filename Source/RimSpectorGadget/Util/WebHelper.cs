using Contracts;
using RimMod;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using UnityEngine.Networking;
using Verse;

namespace RimSpectorMod
{
    internal class WebHelper
    {
        private readonly Settings _settings;
        private readonly EndpointBuilder _endpointBuilder;
        private readonly DebugLogger _debugLogger;
        private readonly DataContractJsonSerializer _serializer;

        public WebHelper(
            Settings settings,
            EndpointBuilder endpointBuilder,
            DebugLogger debugLogger,
            SerializerProvider serializerProvider)
        {
            _settings = settings;
            _endpointBuilder = endpointBuilder;
            _debugLogger = debugLogger;
            _serializer = serializerProvider.GetSerializer();
        }

        public long Post(Payload payload)
        {
            try
            {
                using (var stream = new MemoryStream())
                using (var request = new UnityWebRequest(_endpointBuilder.ConfiguredApiEndpoint, "POST"))
                {
                    _serializer.WriteObject(stream, payload);

                    request.SetRequestHeader("Content-Type", "application/json");
                    request.SetRequestHeader("CLIENT-KEY", _settings._privateKey);
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.uploadHandler = new UploadHandlerRaw(stream.ToArray());
                    request.timeout = 20; // seconds

                    _debugLogger.Log($"[RimSpector]: trying post");
                    request.SendWebRequest();

                    while (!request.isDone)
                    {
                        Thread.Sleep(20);
                    }

                    _debugLogger.Log($"[RimSpector]: POSTed; StatusCode {request.responseCode}; {(request.isNetworkError ? "NetworkError; " : string.Empty)}uploadedBytes {request.uploadedBytes}");

                    return request.responseCode;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error caught while posting payload.");
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
                Log.Error(ex.InnerException.Message);

                return -1;
            }
        }
    }
}
