using Contracts;
using RimMod;
using RimSpectorMod.Mapper;
using RimWorld;
using System;
using System.Collections.Generic;
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
            var pawns = GetPawns();

            var ideos = pawns?
                .GroupBy(p => p.Ideo)
                .OrderByDescending(p => p.Count())
                .Select(p => p.Key)
                .ToList();

            //TODO Add some metadata
            //World seed & settings + colony coordinates
            //Current.Game.storyteller difficulty, settings & name
            //ModLister.AllInstalledMods.Where(m => m.Active).Select(m => m.Name);

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

        private static IEnumerable<Pawn> GetPawns()
        {
            IEnumerable<Pawn> pawns;
            try
            {
                pawns = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists;
            }
            catch (Exception ex)
            {
                Log.Message("RimSpector: PawnsFinder threw. Continuing with empty pawn list.");
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
                Log.Error(ex.InnerException.Message);

                pawns = Enumerable.Empty<Pawn>();
            }
            return pawns;
        }
    }
}
