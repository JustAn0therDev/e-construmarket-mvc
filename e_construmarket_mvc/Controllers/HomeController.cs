using e_construmarket_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace e_construmarket_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // TODO: passar para o arquivo de config
        private readonly string ApiUrlLocalhost = "https://localhost:44370";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("/produto")]
        public async Task<ActionResult> Produto()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}{1}", ApiUrlLocalhost, "/produto"));
            var jsonResult = await response.Content.ReadAsStringAsync();

            return await Task.FromResult<ActionResult>(Ok(jsonResult));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
