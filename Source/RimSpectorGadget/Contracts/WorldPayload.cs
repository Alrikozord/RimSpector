using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [DataContract]
    public class WorldPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Seed { get; set; }
        [DataMember(EmitDefaultValue = false)] public float Coverage { get; set; }
        [DataMember(EmitDefaultValue = false)] public int Rainfall { get; set; }
        [DataMember(EmitDefaultValue = false)] public int Temperature { get; set; }
        [DataMember(EmitDefaultValue = false)] public int Population { get; set; }
    }
}
