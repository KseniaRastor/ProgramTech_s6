using ShopApp.Models;

namespace ShopApp.Services
{
    public interface IProductService
    {
        public Product Add(string description, double price);

        public Product Delete(Guid id);
        /// <summary>
        ///  Здесь должно быть описание изменения
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public Product? Edit(Guid id, string description, double price);

        public Product Get(Guid id);
    }
}
