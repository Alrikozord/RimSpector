using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class StorytellerPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public float? ThreadScale { get; set; }
        [DataMember(EmitDefaultValue = false)] public bool? MapGeneratesHives { get; set; }
        [DataMember(EmitDefaultValue = false)] public float? InsectSpawingRate { get; set; }
        [DataMember(EmitDefaultValue = false)] public float? DeepDrillingInfestations { get; set; }
    }
}
