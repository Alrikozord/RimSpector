using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RimSpectorApi.Contract
{
    [DataContract]
    public class IdeoPayload
    {

        [DataMember] public int Id { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public bool IsFluid { get; set; }
        [DataMember] public int CurrentReformPoints { get; internal set; }
        [DataMember] public int NextReform { get; internal set; }

        [DataMember] public IEnumerable<RolePayload> Roles { get; set; }
        [DataMember] public IEnumerable<string> Memes { get; set; }
        [DataMember] public IEnumerable<PreceptPayload> Precepts { get; set; }
        [DataMember] public IEnumerable<RitualPreceptPayload> Rituals { get; internal set; }
        [DataMember] public IEnumerable<ThingPreceptPayload> Relics { get; internal set; }
        [DataMember] public IEnumerable<WeaponPreceptPayload> Weapons { get; internal set; }
        [DataMember] public IEnumerable<ThingPreceptPayload> Animals { get; internal set; }
        [DataMember] public IEnumerable<ThingPreceptPayload> Apparel { get; internal set; }
    }
    [DataContract]
    public class RolePayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Role { get; set; }
    }

    [DataContract]
    public class PreceptPayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Detail { get; set; }
        [DataMember] public string Impact { get; set; }
    }

    [DataContract]
    public class RitualPreceptPayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public bool IsAnytime { get; set; }
        [DataMember] public string DateTrigger { get; set; }
        [DataMember] public string Impact { get; set; }
    }

    [DataContract]
    public class WeaponPreceptPayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Bonus { get; set; }
        [DataMember] public string Malus { get; set; }
        [DataMember] public string Impact { get; set; }
    }

    [DataContract]
    public class ThingPreceptPayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Item { get; set; }
        [DataMember] public string Impact { get; set; }
    }
}