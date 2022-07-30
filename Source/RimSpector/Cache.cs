
using Microsoft.Extensions.Caching.Memory;
using RimSpectorApi.Contract;

namespace RimSpectorApi
{
    public class Cache
    {
        private readonly MemoryCache _memoryCache;

        public Cache(MemoryCache memory)
        {
            _memoryCache = memory;
        }

        public void Add(Payload payload)
        {
            _memoryCache.Set(payload.Id, payload);
        }

        public bool TryGet(Guid id, out Payload payload)
        {
            return _memoryCache.TryGetValue(id, out payload);
        }
    }
}
