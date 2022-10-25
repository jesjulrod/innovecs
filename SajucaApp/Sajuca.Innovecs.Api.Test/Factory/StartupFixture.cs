using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Sajuca.Innovecs.Application.Mappings;
using Sajuca.Innovecs.Application.Models;
using Sajuca.Innovecs.Application.Utils;
using Sajuca.Innovecs.Client.Api1.Services;
using Sajuca.Innovecs.Client.Api2.Services;
using Sajuca.Innovecs.Client.Api3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sajuca.Innovecs.Api.Test.Factory
{
    public static class StartupFixture
    {
        private static IServiceCollection _services;
        private static Mock<IHttpClientFactory> _clientFactory;

        private static readonly IFirstCompanyService _firstCompanyService;
        private static readonly ISecondCompanyService _secondCompanyService;
        private static readonly IThirdCompanyService _thirdCompanyService;
        private static readonly IMapper _mapper;
        private static readonly List<double> _quotes;

        public static List<double> Quotes { get { return _quotes; } }

        static StartupFixture()
        {
            // generate quotes for all apis and mock content response
            _quotes = new List<double>()
            {
                GeneratorRandom.GetNumberAsync(1.23, 5.34).Result,
                GeneratorRandom.GetNumberAsync(1.23, 5.34).Result,
                GeneratorRandom.GetNumberAsync(1.23, 5.34).Result
            };

            var myProfile = new ApplicationMappingProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(mapperConfiguration);

            var serializeObject = JsonConvert.SerializeObject(new DtoFirstCompanyQuote() { Total = _quotes.ElementAt(0) });
            var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");

            _clientFactory = CreateHttpClientFactory(new Uri("http://mytestFirstCompany.com/"), System.Net.HttpStatusCode.OK, content);
            _firstCompanyService = new FirstCompanyService(_clientFactory.Object);

            serializeObject = JsonConvert.SerializeObject(new DtoSecondCompanyQuote() { Amount = _quotes.ElementAt(1) });
            content = new StringContent(serializeObject, Encoding.UTF8, "application/json");
            _clientFactory = CreateHttpClientFactory(new Uri("http://mytestSecondCompany.com/"), System.Net.HttpStatusCode.OK, content);
            _secondCompanyService = new SecondCompanyService(_clientFactory.Object);

            serializeObject = GetStringContentToXml(_quotes.ElementAt(2));
            content = new StringContent(serializeObject, Encoding.UTF8, "application/xml");
            _clientFactory = CreateHttpClientFactory(new Uri("http://mytestThirdCompany.com/"), System.Net.HttpStatusCode.OK, content);
            _thirdCompanyService = new ThirdCompanyService(_clientFactory.Object);
        }

        public static Quote CreateInstance()
        {
            var package = new Dictionary<string, object>();
            package["height"] = 1;
            package["length"] = 1;
            package["width"] = 1;
            package["weight"] = 1;

            return new Quote()
            {
                ContactAddress = "Cali, Colombia",
                WarehouseAddress = "Cartagena, Colombia",
                PackageDimension = package
            };
        }

        private static string GetStringContentToXml(double value)
        {
            string content = string.Empty;

            content = @"<DtoThirdCompanyQuote xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>" +
                            $"<Quote>{value}</Quote>" +
                       "</DtoThirdCompanyQuote>";

            return content;
        }

        public static IServiceProvider GetServiceProvider()
        {
            _services = new ServiceCollection();
            _services.AddHttpClient();

            _services.AddScoped(x => _mapper);
            _services.AddScoped(x => _firstCompanyService);
            _services.AddScoped(x => _secondCompanyService);
            _services.AddScoped(x => _thirdCompanyService);

            return _services.BuildServiceProvider();
        }

        private static Mock<IHttpClientFactory> CreateHttpClientFactory(Uri url, System.Net.HttpStatusCode statusCode, HttpContent? content = null)
        {
            var httpFactoryMock = new Mock<IHttpClientFactory>();
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )

               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = statusCode,
                   Content = content,
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = url,
            };

            httpFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            return httpFactoryMock;
        }
    }
}
