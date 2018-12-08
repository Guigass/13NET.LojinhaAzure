using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Lojinha.Infrastructure.Redis
{
    public class RedisCache : IRedisCache
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        public RedisCache(IConfiguration config)
        {
            _redis = ConnectionMultiplexer.Connect(config.GetSection("Azure:Redis").Value);

            _db = _redis.GetDatabase();
        }

        public string Get(string key)
        {
            return _db.StringGet(key);
        }

        public void Set(string key, string value)
        {
            _db.StringSet(key, value);
        }
    }
}
