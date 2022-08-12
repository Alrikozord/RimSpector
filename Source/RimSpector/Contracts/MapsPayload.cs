using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RimSpectorApi.Contracts
{
    [DataContract]
    public class MapPayload
    {
        [DataMember(EmitDefaultValue = false)] public string? Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string? Coordinates { get; set; }
    }
}
