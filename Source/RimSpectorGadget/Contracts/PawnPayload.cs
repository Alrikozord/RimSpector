using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class PawnPayload
    {
        [DataMember] public string Id { get; internal set; }
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string FullName { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Gender { get; set; }
        [DataMember(EmitDefaultValue = false)] public AgePayload Age { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Title { get; set; }
        [DataMember(EmitDefaultValue = false)] public RolePayload Role { get; set; }
        [DataMember(EmitDefaultValue = false)] public int Ideo { get; set; }


        [DataMember(EmitDefaultValue = false)] public IEnumerable<ThingPayload> Equipment { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<ThingPayload> Apparel { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<SkillPayload> Skills { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<IncapabilityPayload> Incapabilities { get; set; }


        [DataMember(EmitDefaultValue = false)] public int PsylinkLevel { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<PsycastPayload> Psycasts { get; set; }

        [DataMember(EmitDefaultValue = false)] public IEnumerable<TraitPayload> Traits { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<BackstoryPayload> Backstories { get; set; }
        [DataMember(EmitDefaultValue = false)] public IEnumerable<HealthPayload> Health { get; set; }
    }
    [DataContract]
    public class SkillPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public Passion Passion { get; set; }
        [DataMember(EmitDefaultValue = false)] public int MinLevel { get; set; }
        [DataMember(EmitDefaultValue = false)] public int MaxLevel { get; set; }
        [DataMember(EmitDefaultValue = false)] public int Level { get; set; }
        [DataMember(EmitDefaultValue = false)] public bool Disabled { get; set; }

    }
    [DataContract]
    public class IncapabilityPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
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
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
    }
    [DataContract]
    public class AgePayload
    {
        [DataMember(EmitDefaultValue = false)] public int AgeBiologicalYears { get; set; }
        [DataMember(EmitDefaultValue = false)] public int AgeChronologicalYears { get; set; }
    }
    [DataContract]
    public class TraitPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Desc { get; set; }
    }
    [DataContract]
    public class BackstoryPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Desc { get; set; }
    }
    [DataContract]
    public class HealthPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Part { get; set; }
    }
    [DataContract]
    public class PsycastPayload
    {
        [DataMember(EmitDefaultValue = false)] public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string Desc { get; set; }
    }
}