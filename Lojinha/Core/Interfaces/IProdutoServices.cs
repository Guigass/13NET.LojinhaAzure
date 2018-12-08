using System.Collections.Generic;
using System.Threading.Tasks;
using Lojinha.Core.Models;

namespace Lojinha.Core.Services
{
    public interface IProdutoServices
    {
        Task<Produto> ObterProduto(string id);
        Task<List<Produto>> ObterProdutosAsync();
    }
}