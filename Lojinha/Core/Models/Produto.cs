using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Core.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public Fabricante Fabricante { get; set; }
        public Categoria Categoria { get; set; }
        public string ImagemPrincipalUrl { get; set; }
        public Imagens[] Imagens { get; set; }
        public string[] Tags { get; set; }
    }
}
