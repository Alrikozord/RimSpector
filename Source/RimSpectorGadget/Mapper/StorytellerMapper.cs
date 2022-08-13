
using Contracts;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimSpectorMod.Mapper
{
    internal class StorytellerMapper
    {
        internal static StorytellerPayload Map(Storyteller storyteller)
        {
            var payload = new StorytellerPayload();

            payload.Name = storyteller.def.LabelCap;

            payload.ThreadScale = storyteller.difficulty.threatScale;

            payload.MapGeneratesHives = storyteller.difficulty.allowCaveHives;
            payload.InsectSpawingRate = storyteller.difficultyDef.enemyReproductionRateFactor;
            payload.DeepDrillingInfestations = storyteller.difficultyDef.deepDrillInfestationChanceFactor;

            return payload;
        }
    }
}
