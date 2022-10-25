using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sajuca.Innovecs.Api.Controllers;
using Sajuca.Innovecs.Api.Test.Factory;
using Sajuca.Innovecs.Application.Interfaces;
using Sajuca.Innovecs.Application.Services;
using Sajuca.Innovecs.Client.Api1.Services;
using Sajuca.Innovecs.Client.Api2.Services;
using Sajuca.Innovecs.Client.Api3.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sajuca.Innovecs.Api.Test.Controllers
{
    public class QuoteControllerTest
    {
        private readonly IFirstCompanyService _firstCompanyService;
        private readonly ISecondCompanyService _secondCompanyService;
        private readonly IThirdCompanyService _thirdCompanyService;
        private readonly IMapper _mapper;

        private readonly IQuoteService _quoteService;
        private readonly ILogger<QuoteController> _logger;
        private readonly IServiceProvider _provider;

        public QuoteControllerTest()
        {
            _provider = StartupFixture.GetServiceProvider();
            _firstCompanyService = (IFirstCompanyService)_provider.GetService(typeof(IFirstCompanyService));
            _secondCompanyService = (ISecondCompanyService)_provider.GetService(typeof(ISecondCompanyService));
            _thirdCompanyService = (IThirdCompanyService)_provider.GetService(typeof(IThirdCompanyService));
            _mapper = (IMapper)_provider.GetService(typeof(IMapper));
            _quoteService = new QuoteService(_firstCompanyService, _secondCompanyService, _thirdCompanyService, _mapper);
        }

        [Fact]
        public async Task GetLowestQuote_Successfully()
        {
            var quoteController = new QuoteController(_logger, _quoteService);
            var mockQuoteApis = StartupFixture.Quotes;
            double lowestValue = mockQuoteApis.Min();

            var operation = await quoteController.GetQuote(StartupFixture.CreateInstance());
            var result = operation as OkObjectResult;

            Assert.IsType<double>(result.Value);
            Assert.Equal(lowestValue, result.Value);
        }
    }
}
