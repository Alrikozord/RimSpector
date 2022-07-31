
using Microsoft.Extensions.Caching.Memory;
using RimSpectorApi.Contracts;

namespace RimSpectorApi
{
    public class Cache
    {
        private readonly IMemoryCache _memoryCache;

        public Cache(IMemoryCache memory)
        {
            _memoryCache = memory;
        }

        public void Add(Payload payload)
        {
            _memoryCache.Set(payload.Id, payload,TimeSpan.FromMinutes(5));
        }

        public bool TryGet(Guid id, out Payload payload)
        {
            return _memoryCache.TryGetValue(id, out payload);
        }
    }
}
