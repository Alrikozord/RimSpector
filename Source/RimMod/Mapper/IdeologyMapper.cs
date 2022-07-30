using Contracts;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

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

        public static IEnumerable<IdeoPayload> Map(IEnumerable<Ideo> ideos) => ideos.Select(Map);
        public static IdeoPayload Map(Ideo ideo) => new IdeologyMapper(ideo).Map();

        public IdeoPayload Map()
        {
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
            payload.Animals = MapThingPrecepts<Precept_Animal>();
            payload.Apparel = MapThingPrecepts<Precept_Apparel>();
            //IdeoUIUtility
            
            //TODO PreceptPayload aufbohren

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

        private IEnumerable<ThingPreceptPayload> MapRelicsPrecepts()
        => _ideo.GetAllPreceptsOfType<Precept_Relic>()
           .Where(p => p.def?.visible ?? false)
           .Select(p => new ThingPreceptPayload
           {
               Name = p?.UIInfoSecondLine,
               Item = p?.ThingDef?.LabelCap,
               Impact = p?.def?.impact.ToString(),
           })
           .ToList();


        private IEnumerable<ThingPreceptPayload> MapThingPrecepts<T>()
            where T : Precept
            => _ideo.GetAllPreceptsOfType<T>()
               .Where(p => p.def?.visible ?? false)
               .Select(MapThing)
               .ToList();

        internal static RolePayload Map(Precept_Role role)
            => new RolePayload
            {
                Name = role?.LabelCap,
                Role = role?.def?.LabelCap
            };

        internal static ThingPreceptPayload MapThing(Precept precept)
            => new ThingPreceptPayload
            {
                Name = precept?.UIInfoSecondLine,
                Item = precept?.UIInfoFirstLine,
                Impact = precept?.def?.impact.ToString(),
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

        private ThingPreceptPayload Map(Precept_Relic precept)
           => new ThingPreceptPayload
           {
               Name = precept?.UIInfoSecondLine,
               Item = precept?.LabelCap,
               Impact = precept?.def?.impact.ToString(),
           };

        private ThingPayload Map(Thing thing)
            => new ThingPayload
            {
                Name = thing.LabelCap
            };
    }


}
