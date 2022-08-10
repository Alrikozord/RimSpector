using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using NeoSmart.Caching.Sqlite;

namespace RimSpectorApi
{
    internal static class SqliteCacheExtensions
    {
        public static bool TryGetString(this SqliteCache sqliteCache, string key, out string value)
        {
            value = sqliteCache.GetString(key);

            return value != null;
        }

        public static string GetOrCreateString(this SqliteCache sqliteCache, string key, string value, Action<DistributedCacheEntryOptions>? optionsConfiguration = null)
        {
            if (sqliteCache.TryGetString(key, out var cachedValue))
                return cachedValue;

            if (optionsConfiguration is not null)
            {
                var options = new DistributedCacheEntryOptions();
                optionsConfiguration.Invoke(options);
                sqliteCache.SetString(key, value, options);
            }
            else
            {
                sqliteCache.SetString(key, value);
            }

            return value;

        }
    }
}
