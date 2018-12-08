using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Core.Models
{
    public class CarrinhoItem
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
