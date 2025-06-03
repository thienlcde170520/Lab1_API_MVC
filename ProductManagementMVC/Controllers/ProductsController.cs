using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using Newtonsoft.Json;
using DataAccessObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Services.Interfaces;
using Services.Service;
namespace ProductManagementMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICatergoryService _categoryService;
        private readonly MyStoreContext _context;
        private readonly HttpClient _httpClient = null;
        private string ProductURL = "";

        public ProductsController(ICatergoryService categoryService)
        {
            _categoryService = categoryService;
            _context = new MyStoreContext();
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ProductURL = "https://localhost:7036/api/Products";
        }
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage res = await _httpClient.GetAsync(ProductURL);
            string strData = await res.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(strData);
            return View(products);
        }
        // GET: ProductsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync(ProductURL + "/" + id);
            string strData = await res.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Product product = JsonConvert.DeserializeObject<Product>(strData);
            return View(product);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string strData = System.Text.Json.JsonSerializer.Serialize(p);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await _httpClient.PostAsync(ProductURL, contentData);
                    if (res.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Insert success!";
                    }
                    else
                    {
                        ViewBag.Message = "Insert fail!";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(p);
            }
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync(ProductURL + "/" + id);
            string strData = await res.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Product product = JsonConvert.DeserializeObject<Product>(strData);
            ViewData["CategoryId"] = new SelectList( _categoryService.GetCategories(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product p)
        {
            Console.WriteLine(p.CategoryId);
            try
            {
                if (ModelState.IsValid)
                {
                    string strData = System.Text.Json.JsonSerializer.Serialize(p);
                    var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await _httpClient.PutAsync(ProductURL + "/" + id, contentData);
                    if (res.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Update success!";
                    }
                    else
                    {
                        ViewBag.Message = "Update fail!";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(p);
            }
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync(ProductURL + "/" + id);
            string strData = await res.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Product product = JsonConvert.DeserializeObject<Product>(strData);
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Product p)
        {
            try
            {
                HttpResponseMessage res = await _httpClient.DeleteAsync(ProductURL + "/" + id);
                if (res.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Delete success!";
                }
                else
                {
                    ViewBag.Message = "Delete fail!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(p);
            }
        }
    }
}
