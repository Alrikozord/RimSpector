using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class Payload
    {
        [DataMember] public DateTime Timestamp { get; set; }
        [DataMember] public Guid Id { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<PawnPayload> Pawns { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<IdeoPayload> Ideos { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<HistoryPayload> History { get; set; }
    }
}
