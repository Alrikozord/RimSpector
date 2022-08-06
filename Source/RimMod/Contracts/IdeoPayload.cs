using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class IdeoPayload
    {
        [DataMember] public int Id { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public bool IsFluid { get; set; }
        [DataMember(EmitDefaultValue = false)] public int CurrentReformPoints { get; internal set; }
        [DataMember(EmitDefaultValue = false)] public int NextReform { get; internal set; }

        [DataMember(EmitDefaultValue = false)] public IEnumerable<RolePayload> Roles { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<string> Memes { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<PreceptPayload> Precepts { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<RitualPreceptPayload> Rituals { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<RelicPreceptPayload> Relics { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<WeaponPreceptPayload> Weapons { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<AnimalPreceptPayload> Animals { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<ApparelPreceptPayload> Apparel { get; set; }
    }
    [DataContract]
    public class RolePayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Role { get; set; }
    }

    [DataContract]
    public class PreceptPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Detail { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Impact { get; set; }
    }

    [DataContract]
    public class RitualPreceptPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public bool IsAnytime { get; set; }
        [DataMember(EmitDefaultValue = false)] public string DateTrigger { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Impact { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Type { get; set; }
    }

    [DataContract]
    public class RelicPreceptPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Item { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Impact { get; set; }
    }

    [DataContract]
    public class WeaponPreceptPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Bonus { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Malus { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Impact { get; set; }
    }

    [DataContract]
    public class AnimalPreceptPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Impact { get; set; }
    }

    [DataContract]
    public class ApparelPreceptPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Item { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Impact { get; set; }
    }
}