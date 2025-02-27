using ShopApp.Models;
using System.Security.Cryptography;

namespace ShopApp.Services
{
    public class ProductService : IProductService
    {
        public Product Add(string description, double price)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Description = description,
                Price = price
            };
            return product;
        }

        public Product Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product Edit(Guid id, string description, double price)
        {
            throw new NotImplementedException();
        }

        public Product Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
