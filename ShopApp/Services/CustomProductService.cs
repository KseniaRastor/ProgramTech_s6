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
        private string? DataBaseFilePath { get; set; }

        public CustomProductService(IConfiguration config)
        {
            _productsList = new Dictionary<Guid, Product>();
            _config = config;
            DataBaseFilePath = _config.GetValue<string>("DataBaseFilePath");
            InitFromFile();
        }

        private void InitFromFile()
        {
            if (!File.Exists(DataBaseFilePath)) return;

            var fileData = File.ReadAllText(DataBaseFilePath);
            var products = JsonSerializer.Deserialize<List<Product>>(fileData);

            foreach (var product in products)
            {
                _productsList[product.Id] = product; 
            }
        }
        private void WriteToFile()
        {
            var productList = new List<Product>(_productsList.Values);
            var json = JsonSerializer.Serialize(productList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DataBaseFilePath, json);
        }



        public Product Add(string description, double price)
        {
            _mutex.WaitOne();
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Description = description,
                Price = price
            };
            Console.WriteLine(product.Id);
            _productsList.Add(product.Id, product);
            WriteToFile();
            _mutex.ReleaseMutex();
            return product;
        }

        public Product? Delete(Guid id)
        {
            _mutex?.WaitOne();
            if (_productsList.ContainsKey(id))
            {
                var product = _productsList[id];
                if (_productsList.Remove(id))
                {
                    WriteToFile();
                    _mutex?.ReleaseMutex();
                    return product;
                }
            }
            _mutex.ReleaseMutex();
            return null;
        }

        public Product? Edit(Guid id, string description, double price)
        {
            _mutex.WaitOne();
            if (_productsList.ContainsKey(id))
            {
                var product = _productsList[id];
                product.Description = description;
                product.Price = price;
                WriteToFile();
                _mutex.ReleaseMutex();
                return product;
            }
            _mutex.ReleaseMutex();
            return null;
        }

        public Product? Get(Guid id)
        {
            _mutex?.WaitOne();
            if (_productsList.TryGetValue(id, out var product))
            {
                _mutex.ReleaseMutex();
                return product;
            }
            _mutex?.ReleaseMutex();
            return null;
        }
    }
}
