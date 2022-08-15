using System.Runtime.Serialization;

namespace RimSpectorApi.Contracts
{
    [DataContract]
    public class WorldPayload
    {
        [DataMember(EmitDefaultValue = false)] public string? Seed { get; set; }
        [DataMember(EmitDefaultValue = false)] public float Coverage { get; set; }
        [DataMember(EmitDefaultValue = false)] public int Rainfall { get; set; }
        [DataMember(EmitDefaultValue = false)] public int Temperature { get; set; }
        [DataMember(EmitDefaultValue = false)] public int Population { get; set; }
    }
}
