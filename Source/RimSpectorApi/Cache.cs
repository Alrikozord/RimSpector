
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using NeoSmart.Caching.Sqlite;
using RimSpectorApi.Contracts;

namespace RimSpectorApi
{
    public class Cache
    {
        private readonly MemoryCache _payloadCache;
        private readonly SqliteCache _keyCache;
        public int PayloadCount => _payloadCache.Count;

        public Guid? LastAddedPayload { get; private set; }

        public Cache(MemoryCache payloadCache, SqliteCache keyCache)
        {
            _payloadCache = payloadCache;
            _keyCache = keyCache;
        }

        public void AddPayload(string clientKey, Payload payload)
        {
            string cachedClientKey = _keyCache.GetOrCreateString(
                payload.Id.ToString(),
                clientKey,
                opt => opt.SetSlidingExpiration(TimeSpan.FromDays(30)));

            if (clientKey != cachedClientKey)
                throw new InvalidOperationException("Invalid Key");

            _payloadCache.Set(payload.Id, payload, TimeSpan.FromMinutes(5));

            if (payload.Pawns?.Any() ?? false)
            {
                LastAddedPayload = payload.Id;
            }
        }

        public bool TryGetPayload(Guid id, out Payload payload)
        {
            return _payloadCache.TryGetValue(id, out payload);
        }

        internal bool IsClientKeyValidForPayload(string clientKey, Guid v)
        {
            if (_keyCache.TryGetString(v.ToString(), out string cachedClientKey))
                return cachedClientKey == clientKey;

            return true;
        }
    }
}
