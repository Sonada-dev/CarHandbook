using CarHandbook.API.Models;
using CarHandbook.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace CarHandbook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("CarHandbookApi");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var brandsAndModels = new List<Brand>();
            HttpResponseMessage response = await _httpClient.GetAsync("brands/brands-with-models");

            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                brandsAndModels = JsonSerializer.Deserialize<List<Brand>>(data) ?? new List<Brand>();
            }
            return View(brandsAndModels);
        }

        [HttpGet]
        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateModel(Model? model = null)
        {
            var brands = new List<Brand>();
            HttpResponseMessage response = await _httpClient.GetAsync("brands");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                brands = JsonSerializer.Deserialize<List<Brand>>(data) ?? new List<Brand>();
            }
            return View(new BrandsModelViewModel {Model = model, brands = brands});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBrand([Bind("Id,Name,Active")] Brand brand)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var content = JsonSerializer.Serialize(brand);
                    StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _httpClient.PostAsync("brands", stringContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(brand);
            }
            catch (HttpRequestException)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return View("Error", errorViewModel);
            }
        }

        [HttpGet("UpdateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"brands/{id}");
            Brand? brand = null;
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                brand = JsonSerializer.Deserialize<Brand>(data);
            }

            return View(brand);
        }

        [HttpPost, ActionName("EditBrand")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBrand (int id, [Bind("Id,Name,Active")] Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string content = JsonSerializer.Serialize(brand);
                StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"brands/{id}", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(brand);
        }

        [HttpGet("UpdateModel/{id}")]
        public async Task<IActionResult> UpdateModel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"models/{id}");
            Model? model = null;
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                model = JsonSerializer.Deserialize<Model>(data);
            }

            var brands = new List<Brand>();

            response = await _httpClient.GetAsync("brands");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                brands = JsonSerializer.Deserialize<List<Brand>>(data) ?? new List<Brand>();
            }
            return View(new BrandsModelViewModel { Model = model, brands = brands });
        }

        [HttpPost, ActionName("EditModel")]
        public async Task<IActionResult> UpdateModel(int id, BrandsModelViewModel viewModel)
        {
            if (id != viewModel.Model!.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string content = JsonSerializer.Serialize(viewModel.Model);
                StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"models/{id}", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModel(BrandsModelViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var content = JsonSerializer.Serialize(viewModel.Model);
                    StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _httpClient.PostAsync("models", stringContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("CreateModel", viewModel);
            }
            catch (HttpRequestException)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return View("Error", errorViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBrand(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                HttpResponseMessage response = await _httpClient.DeleteAsync($"brands/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    var errorViewModel = new ErrorViewModel
                    {
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                    };
                    return View("Error", errorViewModel);
                }
            }
            catch (HttpRequestException)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return View("Error", errorViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteModel(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                HttpResponseMessage response = await _httpClient.DeleteAsync($"models/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    var errorViewModel = new ErrorViewModel
                    {
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                    };
                    return View("Error", errorViewModel);
                }
            }
            catch (HttpRequestException)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return View("Error", errorViewModel);
            }
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
