using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RimSpectorApi.Contracts
{
    [DataContract]
    public class IdeoPayload
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public string? Name { get; set; }
        [DataMember] public bool? IsFluid { get; set; }
        [DataMember] public int? CurrentReformPoints { get; set; }
        [DataMember] public int? NextReform { get; set; }

        [DataMember] public IEnumerable<RolePayload> Roles { get; set; } = Enumerable.Empty<RolePayload>();
        [DataMember] public IEnumerable<string> Memes { get; set; } = Enumerable.Empty<string>();
        [DataMember] public IEnumerable<PreceptPayload> Precepts { get; set; } = Enumerable.Empty<PreceptPayload>();
        [DataMember] public IEnumerable<RitualPreceptPayload> Rituals { get; set; } = Enumerable.Empty<RitualPreceptPayload>();
        [DataMember] public IEnumerable<RelicPreceptPayload> Relics { get; set; } = Enumerable.Empty<RelicPreceptPayload>();
        [DataMember] public IEnumerable<WeaponPreceptPayload> Weapons { get; set; } = Enumerable.Empty<WeaponPreceptPayload>();
        [DataMember] public IEnumerable<AnimalPreceptPayload> Animals { get; set; } = Enumerable.Empty<AnimalPreceptPayload>();
        [DataMember] public IEnumerable<ApparelPreceptPayload> Apparel { get; set; } = Enumerable.Empty<ApparelPreceptPayload>();
    }
    [DataContract]
    public class RolePayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? Role { get; set; }
    }

    [DataContract]
    public class PreceptPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? Detail { get; set; }
        [DataMember] public string? Impact { get; set; }
    }

    [DataContract]
    public class RitualPreceptPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public bool IsAnytime { get; set; }
        [DataMember] public string? DateTrigger { get; set; }
        [DataMember] public string? Impact { get; set; }
        [DataMember] public string? Type { get; set; }
    }

    [DataContract]
    public class RelicPreceptPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? Item { get; set; }
        [DataMember] public string? Impact { get; set; }
    }

    [DataContract]
    public class WeaponPreceptPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? Bonus { get; set; }
        [DataMember] public string? Malus { get; set; }
        [DataMember] public string? Impact { get; set; }
    }

    [DataContract]
    public class AnimalPreceptPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? Impact { get; set; }
    }

    [DataContract]
    public class ApparelPreceptPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? Item { get; set; }
        [DataMember] public string? Impact { get; set; }
    }
}