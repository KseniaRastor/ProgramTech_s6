using ShopApp.Models;
using System;
using System.Text.Json;
namespace ShopApp.Services
{
    public class CustomProductService : IProductService
    {
        private Dictionary<Guid, Product> _productsList;
        private readonly IConfiguration _config;
        private readonly Mutex _mutex = new Mutex();
        public string DataBaseFilePath { get; set; }

        public CustomProductService(IConfiguration config)
        {
            _productsList = new Dictionary<Guid, Product>();
            _config = config;
            DataBaseFilePath = _config.GetValue<string>("DataBaseFilePath");
        }



        List<Product> newProduct = new List<Product>();
        public void InitFromFile()
        {
            _mutex.WaitOne();
            var fileData = File.ReadAllText(DataBaseFilePath);
            var products = JsonSerializer.Deserialize<List<Product>>(fileData);

            foreach (var product in products)
            {
                _productsList[product.Id] = product; 
            }
            _mutex.ReleaseMutex();
        }

        private void WriteToFile()
        {
            _mutex.WaitOne();
            var productList = new List<Product>(_productsList.Values);
            var json = JsonSerializer.Serialize(productList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DataBaseFilePath, json);
            _mutex.ReleaseMutex();
        }



        public Product Add(string description, double price)
        {
            InitFromFile();
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Description = description,
                Price = price
            };
            Console.WriteLine(product.Id);
            _productsList.Add(product.Id, product);
            WriteToFile();
            return product;
        }

        public Product? Delete(Guid id)
        {
            InitFromFile();
            if (_productsList.ContainsKey(id))
            {
                var product = _productsList[id];
                if (_productsList.Remove(id))
                {
                    WriteToFile();
                    return product;
                }
            }
            return null;
        }

        public Product Edit(Guid id, string description, double price)
        {
            InitFromFile();
            if (_productsList.ContainsKey(id))
            {
                var product = _productsList[id];
                product.Description = description;
                product.Price = price;
                WriteToFile();
                return product;
            }
            return null;
        }

        public Product? Get(Guid id)
        {
            InitFromFile();
            if (_productsList.TryGetValue(id, out var product)) return product;
            else return null;
        }
    }
}
