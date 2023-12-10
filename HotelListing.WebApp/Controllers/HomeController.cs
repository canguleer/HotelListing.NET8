
using HotelListing.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HotelListing.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string? _apiBaseUrl;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, string? apiBaseUrl)
        {
            _logger = logger;
            this._apiBaseUrl = apiBaseUrl;
            configuration.GetValue<string>("WebAPIBaseUrl");
        }

        public async Task<IActionResult> Index()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            var client = new HttpClient(clientHandler);

            var url = _apiBaseUrl + "/v1/countries/GetAll";

            var dd = await client.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<List<Country>>(dd)!.ToList();

            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}