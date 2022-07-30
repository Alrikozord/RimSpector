using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class PawnPayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string FullName { get; set; }
        [DataMember] public string Gender { get; set; }
        [DataMember] public AgePayload Age { get; set; }
        [DataMember] public string Title { get; set; }
        [DataMember] public RolePayload Role { get; set; }
        [DataMember] public int Ideo { get; set; }


        [DataMember] public IEnumerable<ThingPayload> Equipment { get; set; }
        [DataMember] public IEnumerable<ThingPayload> Apparel { get; set; }
        [DataMember] public IEnumerable<SkillPayload> Skills { get; set; }


        [DataMember] public int PsylinkLevel { get; set; }
        [DataMember] public IEnumerable<PsycastPayload> Psycasts { get; set; }

        [DataMember] public IEnumerable<TraitPayload> Traits { get; set; }
        [DataMember] public IEnumerable<BackstoryPayload> Backstories { get; set; }
        [DataMember] public IEnumerable<HealthPayload> Health { get; set; }

    }
    [DataContract]
    public class SkillPayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public Passion Passion { get; set; }
        [DataMember] public int MinLevel { get; set; }
        [DataMember] public int MaxLevel { get; set; }
        [DataMember] public int Level { get; set; }
        [DataMember] public bool Disabled { get; set; }

    }
    [DataContract]
    public enum Passion : byte
    {
        None,
        Minor,
        Major
    }
    [DataContract]
    public class ThingPayload
    {
        [DataMember] public string Name { get; set; }
    }
    [DataContract]
    public class AgePayload
    {
        [DataMember] public int AgeBiologicalYears { get; set; }
        [DataMember] public int AgeChronologicalYears { get; set; }
    }
    [DataContract]
    public class TraitPayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Desc { get; set; }
    }
    [DataContract]
    public class BackstoryPayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Desc { get; set; }
    }
    [DataContract]
    public class HealthPayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Part { get; set; }
    }
    [DataContract]
    public class PsycastPayload
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Desc { get; set; }
    }
}