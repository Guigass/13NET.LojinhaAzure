using Lojinha.Core.Models;
using Lojinha.Infrastructure.Redis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Core.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private const string _key = "rm330284";
        private readonly IRedisCache _redis;

        public CarrinhoService(IRedisCache redis)
        {
            _redis = redis;
        }
        public void Limpar(string usuario)
        {
            _redis.Set($"{_key}:carrinho:{usuario}", null);
        }

        public void Salvar(string usuario, Carrinho carrinho)
        {
            _redis.Set($"{_key}:carrinho:{usuario}", JsonConvert.SerializeObject(carrinho));
        }

        public Carrinho Obter(string usuario)
        {
            var value = _redis.Get($"{_key}:carrinho:{usuario}");
            if (string.IsNullOrWhiteSpace(value))
            {
                var carrinho = new Carrinho();

                Salvar(usuario, carrinho);

                return carrinho;
            }
            return JsonConvert.DeserializeObject<Carrinho>(value);
        }
    }
}
