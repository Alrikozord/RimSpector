using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RimSpectorApi.Contracts
{
    [DataContract]
    public class PawnPayload
    {
        [DataMember] public string? Id { get; set; }
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? FullName { get; set; }
        [DataMember] public string? Gender { get; set; }
        [DataMember] public AgePayload? Age { get; set; }
        [DataMember] public string? Title { get; set; }
        [DataMember] public RolePayload? Role { get; set; }
        [DataMember] public int? Ideo { get; set; }


        [DataMember] public IEnumerable<ThingPayload> Equipment { get; set; } = Enumerable.Empty<ThingPayload>();
        [DataMember] public IEnumerable<ThingPayload> Apparel { get; set; } = Enumerable.Empty<ThingPayload>();
        [DataMember] public IEnumerable<SkillPayload> Skills { get; set; } = Enumerable.Empty<SkillPayload>();
        [DataMember] public IEnumerable<IncapabilityPayload> Incapabilities { get; set; } = Enumerable.Empty<IncapabilityPayload>();


        [DataMember] public int PsylinkLevel { get; set; } = 0;
        [DataMember] public IEnumerable<PsycastPayload> Psycasts { get; set; } = Enumerable.Empty<PsycastPayload>();

        [DataMember] public IEnumerable<TraitPayload> Traits { get; set; } = Enumerable.Empty<TraitPayload>();
        [DataMember] public IEnumerable<BackstoryPayload> Backstories { get; set; } = Enumerable.Empty<BackstoryPayload>();
        [DataMember] public IEnumerable<HealthPayload> Health { get; set; } = Enumerable.Empty<HealthPayload>();

    }
    [DataContract]
    public class SkillPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public Passion? Passion { get; set; }
        [DataMember] public int? MinLevel { get; set; }
        [DataMember] public int? MaxLevel { get; set; }
        [DataMember] public int? Level { get; set; }
        [DataMember] public bool? Disabled { get; set; }

    }
    [DataContract]
    public class IncapabilityPayload
    {
        [DataMember] public string? Name { get; set; }
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
        [DataMember] public string? Name { get; set; }
    }
    [DataContract]
    public class AgePayload
    {
        [DataMember] public int? AgeBiologicalYears { get; set; }
        [DataMember] public int? AgeChronologicalYears { get; set; }
    }
    [DataContract]
    public class TraitPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? Desc { get; set; }
    }
    [DataContract]
    public class BackstoryPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? Desc { get; set; }
    }
    [DataContract]
    public class HealthPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? Part { get; set; }
    }
    [DataContract]
    public class PsycastPayload
    {
        [DataMember] public string? Name { get; set; }
        [DataMember] public string? Desc { get; set; }
    }
}