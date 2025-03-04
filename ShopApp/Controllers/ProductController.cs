using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using ShopApp.Services;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]         // /[action]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// add new product to the database
        /// </summary>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpPost]
        public Product AddProduct(string description, double price)
        {
            if (string.IsNullOrEmpty(description) || price < 0)
            {
                return null;
            }
            var product = _productService.Add(description, price);
            return product;
        }

        /// <summary>
        /// delete product by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public Product? RemoveProduct(Guid id)
        {
            var product = _productService.Delete(id);
            if (product == null) throw new KeyNotFoundException("Product not found or deleted");
            return product; 
        }

        /// <summary>
        /// change product by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpPut]
        public Product? EditProduct(Guid id, string description, double price)
        {
            if (string.IsNullOrEmpty(description) || price < 0)
            {
                return null;
            }
            var product = _productService.Edit(id, description, price);
            return product;
        }

        /// <summary>
        /// product search
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpGet]
        public Product SearchProduct(Guid id)
        {
            var product = _productService.Get(id);
            if (product == null) throw new KeyNotFoundException("Product not found");
            return product;
        }

    }
}


//[HttpGet]
//[Route("list")]
//public string GetProductInfo()
//{
//    return "hello world";
//}


//public IEnumerable<Product> GetProducts()
//{
//    return Enumerable.Range(1, 5).Select(index => new Product
//    {
//        Id = Guid.NewGuid(),
//        Description = "Information about product",
//        Price = Random.Shared.NextDouble()

//    })
//    .ToArray();
//}