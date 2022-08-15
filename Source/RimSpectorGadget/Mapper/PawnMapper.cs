using Contracts;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RimSpectorMod.Mapper
{
    internal class PawnMapper
    {
        private readonly Pawn _pawn;

        public PawnMapper(Pawn pawn)
        {
            if (pawn is null)
                throw new ArgumentNullException(nameof(pawn));

            _pawn = pawn;
        }

        public static IEnumerable<PawnPayload> Map(IEnumerable<Pawn> pawns) => pawns.Select(Map);
        public static PawnPayload Map(Pawn pawn) => new PawnMapper(pawn).Map();

        public PawnPayload Map()
        {
            var payload = new PawnPayload();
            payload.Id = _pawn.ThingID;
            payload.Name = _pawn.Name?.ToStringShort;
            payload.FullName = _pawn.Name?.ToStringFull;
            payload.Gender = _pawn.gender.ToString();
            payload.Age = new AgePayload
            {
                AgeBiologicalYears = _pawn.ageTracker?.AgeBiologicalYears ?? 0,
                AgeChronologicalYears = _pawn.ageTracker?.AgeChronologicalYears ?? 0
            };
            if (!(Faction.OfEmpire is null))
            {
                payload.Title = _pawn.GetCurrentTitleIn(Faction.OfEmpire)?.GetLabelCapFor(_pawn);
            }
            payload.Role = IdeologyMapper.Map(_pawn.ideo?.Ideo?.GetRole(_pawn));

            payload.Ideo = _pawn.Ideo.id;

            payload.Equipment = _pawn
                .equipment?
                .AllEquipmentListForReading?
                .Select(Map)
                .ToList();

            payload.Apparel = _pawn
                .apparel?
                .WornApparel?
                .Select(Map)
                .ToList();

            payload.Skills = _pawn
                .skills?
                .skills?
                .Select(Map)
                .ToList();

            payload.Incapabilities = WorkTagsFrom(_pawn.CombinedDisabledWorkTags).Select(Map);

            payload.Traits = _pawn.story?.traits?.allTraits?.Select(Map).ToList();

            payload.Backstories = _pawn.story?.AllBackstories?.Select(b => Map(b)).ToList();

            payload.Health = VisibleHediffs(_pawn).Select(Map).ToList();

            payload.PsylinkLevel = _pawn.GetPsylinkLevel();
            payload.Psycasts = _pawn.abilities?.abilities?.Select(Map).ToList();

            return payload;
        }

        private SkillPayload Map(SkillRecord skill)
        => new SkillPayload
        {
            Name = skill.def.defName,
            Disabled = skill.TotallyDisabled,
            Level = skill.Level,
            MaxLevel = SkillRecord.MaxLevel,
            MinLevel = SkillRecord.MinLevel,
            Passion = Map(skill.passion)
        };


        private IncapabilityPayload Map(WorkTags workType)
            => new IncapabilityPayload
            {
                Name = workType.LabelTranslated().CapitalizeFirst()
            };
        private static IEnumerable<WorkTags> WorkTagsFrom(WorkTags tags)
        {
            foreach (WorkTags allSelectedItem in tags.GetAllSelectedItems<WorkTags>())
            {
                if (allSelectedItem != 0)
                {
                    yield return allSelectedItem;
                }
            }
        }

        private HealthPayload Map(Hediff health)
           => new HealthPayload
           {
               Name = health?.LabelCap,
               Part = health?.Part?.LabelCap
           };
        private static IEnumerable<Hediff> VisibleHediffs(Pawn pawn)
        {
            List<Hediff_MissingPart> mpca = pawn.health.hediffSet.GetMissingPartsCommonAncestors();
            int num;
            for (int i = 0; i < mpca.Count; i = num + 1)
            {
                yield return mpca[i];
                num = i;
            }
            IEnumerable<Hediff> enumerable = from d in pawn.health.hediffSet.hediffs
                                             where !(d is Hediff_MissingPart) && d.Visible && (d.def != HediffDefOf.BloodLoss)
                                             select d;
            foreach (Hediff hediff in enumerable)
            {
                yield return hediff;
            }
        }

        private PsycastPayload Map(Ability ability)
          => new PsycastPayload
          {
              Name = ability.def.LabelCap,
              Desc = ability.def.description
          };

        private Contracts.Passion Map(RimWorld.Passion passion)
            => (Contracts.Passion)passion;

        private TraitPayload Map(Trait trait)
          => new TraitPayload
          {
              Name = trait.LabelCap,
              Desc = GetCleanTraitDesc(trait)
          };

        private string GetCleanTraitDesc(Trait trait)
        {
            var desc = trait.TipString(_pawn);

            while (desc.IndexOf('<') >= 0)
            {
                var start = desc.IndexOf('<');
                var end = desc.IndexOf('>');
                var tag = desc.Substring(start, end - start + 1);

                desc = desc.Replace(tag, string.Empty);
            }

            return desc;
        }
        private BackstoryPayload Map(Backstory backstory)
        => new BackstoryPayload
        {
            Name = backstory.TitleCapFor(_pawn.gender),
            Desc = backstory.FullDescriptionFor(_pawn)
        };

        private ThingPayload Map(Thing thing)
            => new ThingPayload
            {
                Name = thing.LabelCap
            };
    }
}
