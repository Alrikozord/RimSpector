using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class ModPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
    }
}
