namespace RimSpectorApi.Contracts
{
    public class Payload
    {
        public DateTime Timestamp { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<PawnPayload>? Pawns { get; set; }
        public IEnumerable<IdeoPayload>? Ideos { get; set; }
        public IEnumerable<HistoryPayload>? History { get; set; }

        public WorldPayload? World { get; set; }
        public StorytellerPayload? Storyteller { get; set; }
        public IEnumerable<ModPayload>? Mods { get; set; }
        public IEnumerable<MapPayload>? Maps { get; set; }
    }
}
