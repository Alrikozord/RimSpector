
using Contracts;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimSpectorMod.Mapper
{
    internal class WorldInfoMapper
    {
        internal static WorldPayload Map(WorldInfo worldInfo)
        {
            var payload = new WorldPayload();

            payload.Seed = worldInfo.seedString;
            payload.Coverage = worldInfo.planetCoverage;
            payload.Rainfall = (int)worldInfo.overallRainfall;
            payload.Temperature = (int)worldInfo.overallTemperature;
            payload.Population = (int)worldInfo.overallPopulation;
            
            return payload;
        }
    }
}
