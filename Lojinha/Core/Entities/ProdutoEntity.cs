using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Core.Entities
{
    public class ProdutoEntity : TableEntity
    {
        public ProdutoEntity()
        {

        }

        public ProdutoEntity(string partitionKey, string rowKey) : base (partitionKey, rowKey)
        {

        }

        public string Produto { get; set; }
    }
}
