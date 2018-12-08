using Lojinha.Core.Models;
using Lojinha.Infrastructure.Redis;
using Lojinha.Infrastructure.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Core.Services
{
    public class ProdutoServices : IProdutoServices
    {
        private readonly IRedisCache _cache;
        private readonly IAzureStorage _storage;

        public ProdutoServices(IRedisCache cache, IAzureStorage storage)
        {
            _cache = cache;
            _storage = storage;
        }

        public async Task<Produto> ObterProduto(string id)
        {
            return await _storage.ObterProdutoAsync(id);
        }

        public async Task<List<Produto>> ObterProdutosAsync()
        {
            var key = "produtos";
            var value = _cache.Get(key);

            if (string.IsNullOrWhiteSpace(value))
            {
                var produtos = await _storage.ObterProdutosAsync();

                _cache.Set(key, JsonConvert.SerializeObject(produtos));

                return produtos;
            }

            return JsonConvert.DeserializeObject<List<Produto>>(value);
        }
    }
}
