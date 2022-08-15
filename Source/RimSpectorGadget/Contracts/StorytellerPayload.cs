using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
