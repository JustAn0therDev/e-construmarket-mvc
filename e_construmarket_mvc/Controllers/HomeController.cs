using e_construmarket_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace e_construmarket_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Métodos Privados

        /// <summary>
        /// Filtra os primeiros dez produtos mais baratos de cada marca. Método usado para não fazer uso de LINQ partes do código mais chamadas,
        /// onde pode haver uma degradação de performance.
        /// </summary>
        /// <param name="produtos">Lista de produtos</param>
        /// <returns>Lista filtrada com os primeiros dez produtos de marcas distintas.</returns>
        private List<Produto> FiltraDezPrimeirosProdutosMaisBaratosDeMarcasDistintas(List<Produto> produtos)
        {
            var listaDeProdutosFiltrada = new List<Produto>();

            listaDeProdutosFiltrada.AddRange(FiltraTeclados(produtos));
            listaDeProdutosFiltrada.AddRange(FiltraMouses(produtos));
            listaDeProdutosFiltrada.AddRange(FiltraFones(produtos));

            return listaDeProdutosFiltrada;
        }

        private List<Produto> FiltraTeclados(List<Produto> produtos)
        {
            var tecladosFiltrados = new List<Produto>();
            List<Produto> apenasTecladosOrdenadosPorPreco = produtos.OrderBy(o => o.Preco).Where(w => w.Nome.ToUpper().Contains("TECLADO")).ToList();

            var marcasDistintas = new List<string>();

            foreach (var produto in apenasTecladosOrdenadosPorPreco)
            {
                if (!marcasDistintas.Contains(produto.Marca.Nome)) {
                    tecladosFiltrados.Add(produto);
                    marcasDistintas.Add(produto.Marca.Nome);
                }
            }

            return tecladosFiltrados.Take(10).ToList();
        }

        private List<Produto> FiltraMouses(List<Produto> produtos)
        {
            var mousesFiltrados = new List<Produto>();
            List<Produto> apenasMousesOrdenadosPorPreco = produtos.OrderBy(o => o.Preco).Where(w => w.Nome.ToUpper().Contains("MOUSE")).ToList();

            var marcasDistintas = new List<string>();

            foreach (var produto in apenasMousesOrdenadosPorPreco)
            {
                if (!marcasDistintas.Contains(produto.Marca.Nome)) {
                    mousesFiltrados.Add(produto);
                    marcasDistintas.Add(produto.Marca.Nome);
                }
            }

            return mousesFiltrados.Take(10).ToList();
        }

        private List<Produto> FiltraFones(List<Produto> produtos)
        {
            var fonesFiltrados = new List<Produto>();
            List<Produto> apenasFonesOrdenadosPorPreco = produtos.OrderBy(o => o.Preco).Where(w => w.Nome.ToUpper().Contains("FONES")).ToList();

            var marcasDistintas = new List<string>();

            foreach (var produto in apenasFonesOrdenadosPorPreco)
            {
                if (!marcasDistintas.Contains(produto.Marca.Nome)) {
                    fonesFiltrados.Add(produto);
                    marcasDistintas.Add(produto.Marca.Nome);
                }
            }

            return fonesFiltrados.Take(10).ToList();
        }

        #endregion

        [HttpGet("/produto")]
        public async Task<ActionResult> Produto()
        {
            var apiUrl = _configuration.GetValue<string>("ApiUrl");

            using var client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}{1}", apiUrl, "/produto"));
            var jsonResult = await response.Content.ReadAsStringAsync();

            List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(jsonResult).ToList();

            List<Produto> produtosFiltrados = FiltraDezPrimeirosProdutosMaisBaratosDeMarcasDistintas(produtos);

            var produtosFiltradosSerializados = JsonConvert.SerializeObject(produtosFiltrados);

            return await Task.FromResult<ActionResult>(Ok(produtosFiltradosSerializados));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
