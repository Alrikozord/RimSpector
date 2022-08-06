
using AutoFixture;
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
            //var payload = new Fixture().Create<Payload>();
            //payload.Id = Guid.Parse("1eaea372-1c46-4ccb-9236-39c0ec374dba");

            //_memoryCache.Set(payload.Id, payload, TimeSpan.FromMinutes(5));
        }

        public void Add(Payload payload)
        {
            _memoryCache.Set(payload.Id, payload, TimeSpan.FromMinutes(5));
        }

        public bool TryGet(Guid id, out Payload payload)
        {
            return _memoryCache.TryGetValue(id, out payload);
        }
    }
}
