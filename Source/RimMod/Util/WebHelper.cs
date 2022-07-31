using Contracts;
using RimMod;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using UnityEngine.Networking;

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
            using (var stream = new MemoryStream())
            using (var request = new UnityWebRequest(_endpointBuilder.ConfiguredEndpoint, "POST"))
            {
                _serializer.WriteObject(stream, payload);
                                
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("ClientKey", _settings._privateKey);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.uploadHandler = new UploadHandlerRaw(stream.ToArray());
                request.timeout = 5; // seconds

                _debugLogger.Log($"RimSpector: trying post");
                request.SendWebRequest();

                while (!request.isDone)
                {
                    Thread.Sleep(1000);
                    _debugLogger.Log($"RimSpector: waiting ...");
                }

                _debugLogger.Log($"RimSpector: posted.");
                _debugLogger.Log($"RimSpector: StatusCode {request.responseCode}");
                _debugLogger.Log($"RimSpector: isNetworkError {request.isNetworkError}");
                _debugLogger.Log($"RimSpector: isHttpError {request.isHttpError}");
                _debugLogger.Log($"RimSpector: uploadedBytes {request.uploadedBytes}");

                return request.responseCode;
            }
        }
    }
}
