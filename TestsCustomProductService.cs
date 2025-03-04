using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit;
using NUnit.Framework.Legacy;
using ShopApp.Services;

namespace TestsShopApp
{
    [TestFixture]
    internal class TestsCustomProductService
    {
        private CustomProductService productService;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.tests.json").Build();
            productService = new CustomProductService(config);
            //productService.InitFromFile();
        }

        [TestCase("Hello" , 10)]
        public void TestAddFunction(string discription, double price)
        {
            var product = productService.Add(discription, price);
            var product2 = productService.Get(product.Id);
            Assert.That(product, Is.EqualTo(product2));
        }
    }
}
