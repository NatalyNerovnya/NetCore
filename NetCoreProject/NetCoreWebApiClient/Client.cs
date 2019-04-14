using Microsoft.Extensions.Configuration;
using NetCoreProject.Common;
using NetCoreWebApiClient.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NetCoreWebApiClient
{
    public class Client : IClient
    {
        private readonly HttpClient _httpClient;
        private const string CategoryUri = "/api/category";
        private const string ProductUri = "/api/product";
        private const string BaseUriConfigurationName = "NetCoreWebApi.URI";

        public Client(IConfiguration configuration)
        {
            var baseUri = configuration.GetSection(BaseUriConfigurationName).Value;
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUri)
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await GetListDataAsync<Category>(CategoryUri);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await GetListDataAsync<Product>(ProductUri);
        }

        private async Task<List<T>> GetListDataAsync<T>(string uri)
        {
            List<T> result = null;
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsJsonAsync<List<T>>();
            }

            return result;
        }
    }
}
