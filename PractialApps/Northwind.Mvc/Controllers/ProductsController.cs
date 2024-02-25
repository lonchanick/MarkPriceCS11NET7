using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Northwind.Mvc.Models;
using Packt.Shared;
using System.Text;

namespace Northwind.Mvc.Controllers;

public class ProductsController : Controller
{

    private readonly IHttpClientFactory clientFactory;//para consumir rest API

    public ProductsController(IHttpClientFactory httpClientFactory)
    {
        clientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Products";
        
        string uri = "/product";

        HttpClient client = clientFactory.CreateClient(
                name: "Northwind.WebApi");

        HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: uri);

        HttpResponseMessage response = await client.SendAsync(request);

        IEnumerable<Product>? products = await response.Content
            .ReadFromJsonAsync<IEnumerable<Product>>();

        return View(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProd(SupplierAndCategory modelFromForm)
    {
        ViewData["Title"] = "Add Product";


        await Out.WriteLineAsync("spot");
        string uri = "/api/Product";

        HttpClient client = clientFactory.CreateClient(
                name: "Northwind.WebApi");

        HttpRequestMessage request = new(method: HttpMethod.Post, requestUri: uri);

        Product product = modelFromForm.product;

        var productSerialized = new StringContent(
            JsonConvert.SerializeObject(product),
            Encoding.UTF8,
            "application/json"
            );

        HttpResponseMessage response = await client.PostAsync(uri, productSerialized);

        //Index();

        return Ok();
    }


    public async Task<IActionResult> AddForm()
    {
        ViewData["Title"] = "Add Product";

        string uri = "/api/Customers/GetSuppliersAndCategories";

        HttpClient client = clientFactory.CreateClient(
                name: "Northwind.WebApi");

        HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: uri);

        HttpResponseMessage response = await client.SendAsync(request);

        IEnumerable<IEnumerable<int>>? model = await response.Content
            .ReadFromJsonAsync<IEnumerable<IEnumerable<int>>>();

        SupplierAndCategory supCat = new()
        {
            product = new(),
            SupAndCat = model
        };

        return View(supCat);

    }
}
