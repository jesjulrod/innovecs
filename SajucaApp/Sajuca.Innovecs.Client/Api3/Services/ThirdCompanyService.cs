using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sajuca.Innovecs.Client.Api3.Entities;
using Sajuca.Innovecs.Client.Api3.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sajuca.Innovecs.Client.Api3.Services
{
    public class ThirdCompanyService : IThirdCompanyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ThirdCompanyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ThirdCompanyQuote> GetQuote(string source, string destination, Package package)
        {
            var serializeObject = JsonConvert.SerializeObject(new { source, destination, package });
            var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");

            using var httpClient = _httpClientFactory.CreateClient();
            var authToken = Encoding.ASCII.GetBytes($"admin3:Pa$$WoRd3:thirdApi");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

            HttpResponseMessage responseMessage = await httpClient.PostAsync("https://localhost:44346/third/quote.xml", content);
            string response = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                if (response.TrimStart().StartsWith("<"))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(response);
                    response = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
                }

                var jsonObject = JObject.Parse(response);
                string result = jsonObject.Properties().Where(x => x.Name == "Quote").FirstOrDefault()?.Value.ToString();
                return new ThirdCompanyQuote() { Quote = Convert.ToDouble(result) };
            }

            return default;
        }
    }
}
