using RimSpectorApi.Contract;
using System;
using System.Collections.Generic;

namespace RimSpectorApi.Contract
{
    public class Payload
    {
        public DateTime Timestamp { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<PawnPayload> Pawns { get; set; }
        public IEnumerable<IdeoPayload> Ideos { get; set; }
        public IEnumerable<HistoryPayload> History { get; set; }
    }
}
