
using AutoFixture;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using RimSpectorApi.Contracts;

namespace RimSpectorApi
{
    public class Cache
    {
        private readonly IMemoryCache _payloadCache;
        private readonly IMemoryCache _keyCache;

        public Cache(IMemoryCache payloadCache, IMemoryCache keyCache)
        {
            _payloadCache = payloadCache;
            _keyCache = keyCache;
        }

        public void Add(string clientKey, Payload payload)
        {
            string cachedClientKey = _keyCache.GetOrCreate(payload.Id, entry =>
            {
                entry.SetValue(clientKey);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return clientKey;
            }); 

            if (clientKey != cachedClientKey)
                throw new InvalidOperationException("Invalid Key");

            _payloadCache.Set(payload.Id, payload, TimeSpan.FromMinutes(5));
        }

        public bool TryGet(Guid id, out Payload payload)
        {
            return _payloadCache.TryGetValue(id, out payload);
        }

        internal bool CheckKey(string clientKey, Guid v)
        {
            if (_keyCache.TryGetValue(v, out string cachedClientKey))
                return cachedClientKey == clientKey;

            // Key not in use, good to go.
            //TODO Use some persisted cache.
            return true;
        }
    }
}
