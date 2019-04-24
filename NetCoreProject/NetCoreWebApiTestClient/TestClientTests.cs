using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace NetCoreWebApiTestClient
{
    [TestFixture]
    public class TestClientTests
    {
        private TestClient _client;

        [SetUp]
        public void TestClientTestsSetup()
        {
            var url = @"https://localhost:44336";
            _client = new TestClient(url, new HttpClient());
        }

        [Test]
        public async Task GetCategoriesTest()
        {
            var result = await _client.GetAllAsync();

            foreach (var category in result)
            {
                Console.WriteLine($"{category.Id} : {category.Name}");
            }
        }

        [Test]
        public async Task GetProductsTest()
        {
            var result = await _client.GetAsync();

            foreach (var product in result)
            {
                Console.WriteLine($"{product.Id} : {product.Name} - {product.UnitPrice}");
            }
        }
    }
}
