using ShopApp.Models;

namespace ShopApp.Services
{
    public interface IProductService
    {
        public Product Add(string description, double price);

        public Product Delete(Guid id);

        public Product Edit(Guid id, string description, double price);

        public Product Get(Guid id);
    }
}
