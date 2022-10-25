using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Sajuca.Innovecs.Client.Api2.Models;
using Sajuca.Innovecs.Client.Api2.Entities;
using Sajuca.Innovecs.Client.Api2.Services;
using System;

namespace Sajuca.Innovecs.Client.Api1.Services
{
    public class SecondCompanyService : ISecondCompanyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SecondCompanyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SecondCompanyQuote> GetQuote(string consignor, string consignee, Cartons cartons)
        {
            var serializeObject = JsonConvert.SerializeObject(new { consignor, consignee, cartons });
            var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");

            using var httpClient = _httpClientFactory.CreateClient();
            var authToken = Encoding.ASCII.GetBytes($"admin2:Pa$$WoRd2:secondApi");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

            HttpResponseMessage responseMessage = await httpClient.PostAsync("https://localhost:44346/second/quote", content);
            string response = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                var value = JsonConvert.DeserializeObject<SecondCompanyQuote>(response);
                return JsonConvert.DeserializeObject<SecondCompanyQuote>(response);
            }

            return default;
        }
    }
}
