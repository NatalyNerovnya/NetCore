using Microsoft.Extensions.Configuration;
using NetCoreProject.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NetCoreWebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            RunAsync(configuration).GetAwaiter().GetResult();
            Console.ReadLine();
        }

        private static async Task RunAsync(IConfiguration configuration)
        {
            IClient client = new Client(configuration);

            
            var categories = await client.GetCategoriesAsync();
            ShowCategories(categories);
            Console.WriteLine();

            var products = await client.GetProductsAsync();
            ShowProducts(products);
        }

        private static void ShowCategories(IEnumerable<Category> categories)
        {
            Console.WriteLine("Categories: ");
            foreach (var category in categories)
            {
                Console.WriteLine($"CategoryID = {category.Id}, CategoryName = {category.Name}");
            }
        }

        private static void ShowProducts(IEnumerable<Product> products)
        {
            Console.WriteLine("Products: ");
            foreach (var product in products)
            {
                Console.WriteLine($"ProductId = {product.Id}, ProductName = {product.Name}");
            }
        }
    }
}
