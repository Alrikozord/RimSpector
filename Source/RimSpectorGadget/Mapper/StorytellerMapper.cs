
using Contracts;
using RimWorld;

namespace RimSpectorMod.Mapper
{
    internal class StorytellerMapper
    {
        internal static StorytellerPayload Map(Storyteller storyteller)
        {
            var payload = new StorytellerPayload();

            payload.Name = storyteller.def?.LabelCap;

            payload.ThreadScale = storyteller.difficulty?.threatScale;

            payload.MapGeneratesHives = storyteller.difficulty?.allowCaveHives;
            payload.InsectSpawingRate = storyteller.difficultyDef?.enemyReproductionRateFactor;
            payload.DeepDrillingInfestations = storyteller.difficultyDef?.deepDrillInfestationChanceFactor;

            return payload;
        }
    }
}
