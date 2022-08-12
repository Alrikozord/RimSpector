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
        [DataMember(EmitDefaultValue = false)] public string Seed { get; internal set; }
        [DataMember(EmitDefaultValue = false)] public float Coverage { get; internal set; }
        [DataMember(EmitDefaultValue = false)] public int Rainfall { get; internal set; }
        [DataMember(EmitDefaultValue = false)] public int Temperature { get; internal set; }
        [DataMember(EmitDefaultValue = false)] public int Population { get; internal set; }
    }
}
