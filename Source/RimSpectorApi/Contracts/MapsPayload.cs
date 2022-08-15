using System.Runtime.Serialization;

namespace RimSpectorApi.Contracts
{
    [DataContract]
    public class MapPayload
    {
        [DataMember(EmitDefaultValue = false)] public string? Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string? Coordinates { get; set; }
    }
}
