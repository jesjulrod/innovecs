using Newtonsoft.Json;
using Sajuca.Innovecs.Client.Api1.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Sajuca.Innovecs.Client.Api1.Models;
using System;

namespace Sajuca.Innovecs.Client.Api1.Services
{
    public class FirstCompanyService : IFirstCompanyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FirstCompanyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FirstCompanyQuote> GetQuote(string contactAddress, string warehouseAddress, PackageDimension dimensions)
        {
            var serializeObject = JsonConvert.SerializeObject(new { contactAddress, warehouseAddress, dimensions });
            var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");

            using var httpClient = _httpClientFactory.CreateClient();
            var authToken = Encoding.ASCII.GetBytes($"admin1:Pa$$WoRd1:firstApi");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

            HttpResponseMessage responseMessage = await httpClient.PostAsync("https://localhost:44346/first/quote", content);
            string response = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                var value = JsonConvert.DeserializeObject<FirstCompanyQuote>(response);
                return JsonConvert.DeserializeObject<FirstCompanyQuote>(response);
            }

            return default;
        }
    }
}
