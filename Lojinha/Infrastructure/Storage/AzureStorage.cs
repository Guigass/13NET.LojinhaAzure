using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lojinha.Core.Entities;
using Lojinha.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Lojinha.Infrastructure.Storage
{
    public class AzureStorage : IAzureStorage
    {
        private readonly CloudStorageAccount _account;
        private readonly CloudTableClient _tableClient;

        public AzureStorage(IConfiguration config)
        {
            _account = CloudStorageAccount.Parse(config.GetSection("Azure:Storage").Value);

            _tableClient = _account.CreateCloudTableClient();
        }

        public void AddProduto(Produto produto)
        {
            var json = JsonConvert.SerializeObject(produto);

            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            var entity = new ProdutoEntity("13net", produto.Id.ToString());
            entity.Produto = json;

            var operation = TableOperation.Insert(entity);
            table.ExecuteAsync(operation);
        }

        public async Task<Produto> ObterProdutoAsync(string id)
        {
            // Cria uma referência para nossa table
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            var retrieveOperation = TableOperation.Retrieve<ProdutoEntity>("13net", id);

            // Execute the retrieve operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            return JsonConvert.DeserializeObject<Produto>(((ProdutoEntity)retrievedResult.Result).Produto);
        }


        public async Task<List<Produto>> ObterProdutosAsync()
        {
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            var query = new TableQuery<ProdutoEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "13net"));

            TableContinuationToken token = null;

            var segment = await table.ExecuteQuerySegmentedAsync(query, token);
            var produtosEntity = segment.ToList();

            return produtosEntity
                .Where(p => p.Produto != null)
                .Select(x => JsonConvert.DeserializeObject<Produto>(x.Produto))
                .ToList();
        }
    }
}
