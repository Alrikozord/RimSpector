
using Contracts;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimSpectorMod.Mapper
{
    internal class MapsMapper
    {
        internal static IEnumerable<MapPayload> Map(IEnumerable<Map> mapsData)
            => mapsData
                .Select(m => m.Parent as Settlement)
                .Where(m => !(m is null))
                .Select(Map);
        internal static MapPayload Map(Settlement mapData)
        {
            var payload = new MapPayload();

            payload.Name = mapData.Name;

            var coords = Find.WorldGrid.LongLatOf(mapData.Tile);            
            payload.Coordinates = $"{coords.x.ToStringLongitude()} {coords.y.ToStringLatitude()}";             


            return payload;
        }
    }
}
