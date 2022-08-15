
using Contracts;
using System.Collections.Generic;
using System.Linq;
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
