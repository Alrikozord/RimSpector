
using Contracts;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimSpectorMod.Mapper
{
    internal class ModsMapper
    {
        internal static IEnumerable<ModPayload> Map(IEnumerable<ModMetaData> modData)
            => modData
                .Where(m => m.Active)
                .Select(Map);
        internal static ModPayload Map(ModMetaData modData)
        {
            var payload = new ModPayload();

            payload.Name = modData.Name;

            return payload;
        }
    }
}
