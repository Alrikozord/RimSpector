using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [DataContract]
    public class ModPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
    }
}
