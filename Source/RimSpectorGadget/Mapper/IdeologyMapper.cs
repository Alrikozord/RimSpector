using Contracts;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RimSpectorMod.Mapper
{
    internal class IdeologyMapper
    {
        private readonly Ideo _ideo;

        public IdeologyMapper(Ideo ideo)
        {
            if (ideo is null)
                throw new ArgumentNullException(nameof(ideo));

            _ideo = ideo;
        }

        public static IEnumerable<IdeoPayload> Map(IEnumerable<Ideo> ideos)
            => ideos
                .Select(Map)
                .Where(p => !(p is null));
        public static IdeoPayload Map(Ideo ideo) => new IdeologyMapper(ideo).Map();

        public IdeoPayload Map()
        {
            if (_ideo.createdFromNoExpansionGame)
                return null;

            var payload = new IdeoPayload();
            payload.Id = _ideo.id;
            payload.Name = _ideo.name;
            payload.IsFluid = _ideo.Fluid;
            payload.CurrentReformPoints = _ideo.development?.Points ?? 0;
            payload.NextReform = _ideo.development?.NextReformationDevelopmentPoints ?? 0;
            payload.Roles = _ideo.cachedPossibleRoles?.Select(Map).ToList();
            payload.Memes = _ideo.memes?.Where(m => m.category != MemeCategory.Structure).Select(m => m.label);

            payload.Precepts = _ideo.GetAllPreceptsOfType<Precept>()
                .Where(p => p.GetType() == typeof(Precept))
                .Where(p => p.def?.visible ?? false)
                .Select(Map)
                .ToList();
            payload.Rituals = MapRitualPrecepts();
            payload.Relics = MapRelicsPrecepts();
            payload.Weapons = MapWeaponPrecepts();
            payload.Animals = MapAnimalsPrecepts();
            payload.Apparel = MapApparelPrecepts();
            //IdeoUIUtility

            return payload;
        }

        private IEnumerable<RitualPreceptPayload> MapRitualPrecepts()
         => _ideo.GetAllPreceptsOfType<Precept_Ritual>()
            .Where(p => p.def?.visible ?? false)
            .Select(Map)
            .ToList();

        private IEnumerable<WeaponPreceptPayload> MapWeaponPrecepts()
         => _ideo.GetAllPreceptsOfType<Precept_Weapon>()
            .Where(p => p.def?.visible ?? false)
            .Select(Map)
            .ToList();

        private IEnumerable<RelicPreceptPayload> MapRelicsPrecepts()
        => _ideo.GetAllPreceptsOfType<Precept_Relic>()
           .Where(p => p.def?.visible ?? false)
           .Select(Map)
           .ToList();

        private IEnumerable<AnimalPreceptPayload> MapAnimalsPrecepts()
        => _ideo.GetAllPreceptsOfType<Precept_Animal>()
           .Where(p => p.def?.visible ?? false)
           .Select(Map)
           .ToList();

        private IEnumerable<ApparelPreceptPayload> MapApparelPrecepts()
         => _ideo.GetAllPreceptsOfType<Precept_Apparel>()
            .Where(p => p.def?.visible ?? false)
            .Select(Map)
            .ToList();

        internal static RolePayload Map(Precept_Role role)
            => new RolePayload
            {
                Name = role?.LabelCap,
                Role = role?.def?.LabelCap
            };


        internal static PreceptPayload Map(Precept precept)
            => new PreceptPayload
            {
                Name = precept?.UIInfoFirstLine,
                Detail = precept?.UIInfoSecondLine,
                Impact = precept?.def?.impact.ToString(),
            };

        private RitualPreceptPayload Map(Precept_Ritual precept)
        {
            var trigger = precept?.obligationTriggers?.OfType<RitualObligationTrigger_Date>().FirstOrDefault();

            return new RitualPreceptPayload
            {
                Name = precept?.LabelCap,
                IsAnytime = precept?.isAnytime ?? false,
                DateTrigger = trigger?.DateString,
                Type = precept?.UIInfoFirstLine,
                Impact = precept?.def?.impact.ToString(),
            };
        }

        private WeaponPreceptPayload Map(Precept_Weapon precept)
            => new WeaponPreceptPayload
            {
                Name = precept?.LabelCap,
                Bonus = precept?.UIInfoFirstLine,
                Malus = precept?.UIInfoSecondLine,
                Impact = precept?.def?.impact.ToString(),
            };

        private RelicPreceptPayload Map(Precept_Relic precept)
           => new RelicPreceptPayload
           {
               Name = precept?.UIInfoSecondLine,
               Item = precept?.ThingDef?.LabelCap,
               Impact = precept?.def?.impact.ToString(),
           };

        private ApparelPreceptPayload Map(Precept_Apparel precept)
          => new ApparelPreceptPayload
          {
              Name = precept?.UIInfoSecondLine,
              Item = precept?.UIInfoFirstLine,
              Impact = precept?.def?.impact.ToString(),
          };

        private AnimalPreceptPayload Map(Precept_Animal precept)
         => new AnimalPreceptPayload
         {
             Name = precept?.UIInfoFirstLine,
             Impact = precept?.def?.impact.ToString(),
         };
    }


}
