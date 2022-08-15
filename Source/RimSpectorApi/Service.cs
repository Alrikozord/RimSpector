using RimSpectorApi.Contracts;

namespace RimSpectorApi
{
    public class Service
    {
        private readonly Cache _cache;

        public Service(Cache cache)
        {
            _cache = cache;
        }

        public void Add(string clientKey, Payload payload)
            => _cache.AddPayload(clientKey, payload);

        public bool TryGetFullPayload(Guid id, out Payload? payload)
            => _cache.TryGetPayload(id, out payload);

        public Payload? GetFullPayload(Guid id)
        {
            if (_cache.TryGetPayload(id, out var payload))
                return payload;

            throw new KeyNotFoundException();
        }

        public bool TryGetPawns(Guid id, out IEnumerable<PawnPayload> pawns)
        {
            pawns = Enumerable.Empty<PawnPayload>();
            if (_cache.TryGetPayload(id, out var payload))
            {
                pawns = payload.Pawns ?? Enumerable.Empty<PawnPayload>();
                return pawns.Any();
            }

            return false;
        }

        public bool TryGetPawn(Guid id, string pawnId, out PawnPayload? pawn)
        {
            if (_cache.TryGetPayload(id, out var payload))
            {
                pawn = payload.Pawns?.FirstOrDefault(p => p.Id == pawnId);
                return pawn is not null;
            }

            pawn = null;
            return false;
        }

        public PawnPayload GetPawn(Guid id, string pawnId)
        {
            if (_cache.TryGetPayload(id, out var payload))
                return payload.Pawns?.FirstOrDefault(p => p.Id == pawnId)
                    ?? throw new KeyNotFoundException();

            throw new KeyNotFoundException();
        }

        public bool TryGetIdeos(Guid id, out IEnumerable<IdeoPayload> ideos)
        {
            ideos = Enumerable.Empty<IdeoPayload>();
            if (_cache.TryGetPayload(id, out var payload))
            {
                ideos = payload.Ideos ?? Enumerable.Empty<IdeoPayload>();
                return ideos.Any();
            }

            return false;
        }

        public bool TryGetIdeo(Guid id, int ideoId, out IdeoPayload? ideo)
        {
            if (_cache.TryGetPayload(id, out var payload))
            {
                ideo = payload.Ideos?.FirstOrDefault(p => p.Id == ideoId);
                return ideo is not null;
            }

            ideo = null;
            return false;
        }

        public IdeoPayload GetIdeo(Guid id, int ideoId)
        {
            if (_cache.TryGetPayload(id, out var payload))
                return payload.Ideos?.FirstOrDefault(p => p.Id == ideoId)
                    ?? throw new KeyNotFoundException();

            throw new KeyNotFoundException();
        }


        public bool TryGetWorld(Guid id, out WorldPayload? world)
        {
            world = null;
            if (_cache.TryGetPayload(id, out var payload))
            {
                world = payload.World;
                return world != null;
            }

            return false;
        }

        public bool TryGetStoryteller(Guid id, out StorytellerPayload? storyteller)
        {
            storyteller = null;
            if (_cache.TryGetPayload(id, out var payload))
            {
                storyteller = payload.Storyteller;
                return storyteller != null;
            }

            return false;
        }

        public bool TryGetMods(Guid id, out IEnumerable<ModPayload> mods)
        {
            mods = Enumerable.Empty<ModPayload>();
            if (_cache.TryGetPayload(id, out var payload))
            {
                mods = payload.Mods ?? Enumerable.Empty<ModPayload>();
                return mods.Any();
            }

            return false;
        }

        public bool TryGetMaps(Guid id, out IEnumerable<MapPayload> maps)
        {
            maps = Enumerable.Empty<MapPayload>();
            if (_cache.TryGetPayload(id, out var payload))
            {
                maps = payload.Maps ?? Enumerable.Empty<MapPayload>();
                return maps.Any();
            }

            return false;
        }

    }
}
