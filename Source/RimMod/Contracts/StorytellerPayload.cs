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
        [DataMember(EmitDefaultValue = false)] public string Name { get; internal set; }
        [DataMember(EmitDefaultValue = false)] public float ThreadScale { get; internal set; }
        [DataMember(EmitDefaultValue = false)] public bool MapGeneratesHives { get; internal set; }
        [DataMember(EmitDefaultValue = false)] public float InsectSpawingRate { get; internal set; }
        [DataMember(EmitDefaultValue = false)] public float DeepDrillingInfestations { get; internal set; }
    }
}
