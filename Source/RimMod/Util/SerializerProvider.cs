using Contracts;
using System.Runtime.Serialization.Json;

namespace RimSpectorMod
{
    internal class SerializerProvider
    {
        public DataContractJsonSerializer GetSerializer()
            => new DataContractJsonSerializer(typeof(Payload), new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-ddTHH:mm:ss"),
                IgnoreExtensionDataObject = true
            });
    }
}
