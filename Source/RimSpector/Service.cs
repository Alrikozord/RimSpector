using Microsoft.AspNetCore.Mvc;
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
            => _cache.Add(clientKey, payload);

        public bool TryGetFullPayload(Guid id, out Payload? payload)
            => _cache.TryGet(id, out payload);

        public Payload? GetFullPayload(Guid id)
        {
            if (_cache.TryGet(id, out var payload))
                return payload;

            throw new KeyNotFoundException();
        }

        public bool TryGetPawns(Guid id, out IEnumerable<PawnPayload> pawns)
        {
            pawns = Enumerable.Empty<PawnPayload>();
            if (_cache.TryGet(id, out var payload))
            {
                pawns = payload.Pawns ?? Enumerable.Empty<PawnPayload>();
                return pawns.Any();
            }

            return false;
        }

        public bool TryGetPawn(Guid id, string pawnId, out PawnPayload? pawn)
        {
            if (_cache.TryGet(id, out var payload))
            {
                pawn = payload.Pawns?.FirstOrDefault(p => p.Id == pawnId);
                return pawn is not null;
            }

            pawn = null;
            return false;
        }

        public PawnPayload GetPawn(Guid id, string pawnId)
        {
            if (_cache.TryGet(id, out var payload))            
                return payload.Pawns?.FirstOrDefault(p => p.Id == pawnId)
                    ?? throw new KeyNotFoundException();            

            throw new KeyNotFoundException();
        }

        public bool TryGetIdeos(Guid id, out IEnumerable<IdeoPayload> ideos)
        {
            ideos = Enumerable.Empty<IdeoPayload>();
            if (_cache.TryGet(id, out var payload))
            {
                ideos = payload.Ideos ?? Enumerable.Empty<IdeoPayload>();
                return ideos.Any();
            }

            return false;
        }

        public bool TryGetIdeo(Guid id, int ideoId, out IdeoPayload? ideo)
        {
            if (_cache.TryGet(id, out var payload))
            {
                ideo = payload.Ideos?.FirstOrDefault(p => p.Id == ideoId);
                return ideo is not null;
            }

            ideo = null;
            return false;
        }

        public IdeoPayload GetIdeo(Guid id, int ideoId)
        {
            if (_cache.TryGet(id, out var payload))
                return payload.Ideos?.FirstOrDefault(p => p.Id == ideoId)
                    ?? throw new KeyNotFoundException();

            throw new KeyNotFoundException();
        }
    }
}
