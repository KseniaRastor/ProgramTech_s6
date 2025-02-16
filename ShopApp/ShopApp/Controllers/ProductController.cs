using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;


namespace ShopApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    { 

        private Dictionary<Guid, Product> _products;
        public ProductController()
        {
            _products = new Dictionary<Guid, Product>();
            this.InitProducts();
        }

        private void InitProducts()
        {
            foreach(var item in Enumerable.Range(0, 100))
            {
                var product = new Product()
                {
                    Description = $"Information about {item}",
                    Id = Guid.NewGuid(),
                    Price = Random.Shared.NextDouble()
                };
                _products.Add(product.Id, product);
            }
        }

        ///// <summary>
        ///// Get Products
        ///// </summary>
        ///// <returns></returns>    
        //[HttpGet]
        //public IEnumerable<Product> GetProducts(int start = 0, int take = 10) 
        //{
        //    return this._products.Skip(0).Take(take).Select(x => x.Value).ToArray();
        //}


        ///// <summary>
        ///// Create a product with the specified description
        ///// </summary>
        ///// <param name="description"></param>
        ///// <param name="price"></param>
        ///// <returns></returns>
        //[HttpPost]
        ////[Route("create")]
        //public Guid CreateProduct(string description, double price)
        //{
        //    if (string.IsNullOrEmpty(description) || price < 0)
        //    {
        //        return Guid.Empty;
        //    }
        //    var product = new Product()
        //    {
        //        Id = Guid.NewGuid(),
        //        Description = description,
        //        Price = price
        //    };
        //    if(this._products.TryAdd(product.Id, product))
        //    {
        //        return product.Id;
        //    }
        //    return Guid.Empty;
        //}


        //[HttpGet]
        ////[Route("product")]
        //public Product? GetProductById(Guid id)
        //{
        //    if(this._products.ContainsKey(id)) return this._products[id];
        //    return null;
        //}




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
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Description = description,
                Price = price
            };
            if (this._products.TryAdd(product.Id, product))
            {
                return product;
            }
            return null;
        }

        /// <summary>
        /// delete product by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public Product? RemoveProduct(Guid id)
        {
            if (this._products.ContainsKey(id))
            {
                var product = this._products[id];
                if (this._products.Remove(id))
                {
                    return product;
                }
            }
            return null;
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

            if (this._products.ContainsKey(id))
            {
                var product = this._products[id];
                
                product.Description = description;
                product.Price = price; 
                
                return product; 
            }
            return null;
        }

        /// <summary>
        /// product search
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpGet]
        public Product? SearchProduct(Guid? id, string? description, double? price)
        {

            if (!id.HasValue || (price<0) || (string.IsNullOrEmpty(description))){
                return null;
            }

            foreach (var product in this._products.Values)
            {
                bool overlap = false;

                if ((product.Id == id.Value) || (product.Description == description) || (product.Price == price.Value))
                {
                    return product;
                }
            }
            return null;

            //if (string.IsNullOrEmpty(description) || price < 0)
            //{
            //    return null;
            //}
            //if (this._products.ContainsKey(id))
            //{
            //    var product = this._products[id];
            //    if (product.Description == description & product.Price == price) { 
            //        return product;
            //    };                
            //}
            //return null;
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