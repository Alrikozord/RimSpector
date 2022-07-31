using Contracts;
using RimMod;
using RimSpectorMod.Mapper;
using RimWorld;
using System;
using System.Linq;
using Verse;

namespace RimSpectorMod
{
    internal class PayloadBuilder
    {
        private readonly Settings _settings;

        public PayloadBuilder(Settings settings)
        {
            _settings = settings;
        }

        public Payload Build()
        {
            try
            {
                var pawns = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists;

                var ideos = pawns?
                    .GroupBy(p => p.Ideo)
                    .OrderByDescending(p => p.Count())
                    .Select(p => p.Key)
                    .ToList();

                var payload = new Payload();
                payload.Timestamp = DateTime.UtcNow;
                payload.Id = Guid.Parse(_settings?._id);

                if (pawns?.Any() ?? false)
                {
                    payload.Pawns = PawnMapper.Map(pawns).ToList();
                    payload.Ideos = IdeologyMapper.Map(ideos).ToList();
                    //payload.History = GetHistoryPayload();
                }
                return payload;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
                Log.Error(ex.InnerException.Message);

                throw;
            }
        }
    }
}
