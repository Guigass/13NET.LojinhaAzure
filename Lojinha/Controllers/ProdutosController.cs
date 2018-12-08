using AutoMapper;
using Lojinha.Core.Models;
using Lojinha.Core.Services;
using Lojinha.Core.ViewModels;
using Lojinha.Infrastructure.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly IProdutoServices _produtoServices;
        private readonly IMapper _mapper;
        public ProdutosController(IProdutoServices produtoServices, IMapper mapper)
        {
            _produtoServices = produtoServices;
            _mapper = mapper;
        }

        public IActionResult Create()
        {
            var produto = new Produto
            {
                Id = 330284,
                Nome = "Smart TV Samsung 32",
                Descricao = "Tv Samsung",
                Preco = 1000,
                Categoria = new Categoria
                {
                    Id = 10,
                    Nome = "Smart TVs"
                },
                Fabricante = new Fabricante
                {
                    Id = 10,
                    Nome = "Samsung"
                },
                ImagemPrincipalUrl = "https://how-to.watch/wp-content/uploads/2017/12/Samsung-Smart-TV-2.jpg",
                Tags = new[] { "tv", "samsung", "smarttv" }
            };

            //_produtoServices.AddProduto(produto);

            return Content("OK");
        }

        public async Task<IActionResult> List()
        {
            var produtos = await _produtoServices.ObterProdutosAsync();
            var vm = _mapper.Map<List<ProdutoViewModel>>(produtos);

            return View(vm);
        }

        public async Task<IActionResult> Details(string id)
        {
            //Pega a lista de produtos cacheada no redis
            var produto = await _produtoServices.ObterProduto(id);

            //Usa o automapper pra convertar as listas
            var vm = _mapper.Map<ProdutoViewModel>(produto);

            return View(vm);
        }
    }
}
