using AutoMapper;
using Sajuca.Innovecs.Application.Interfaces;
using Sajuca.Innovecs.Application.Models;
using Sajuca.Innovecs.Client.Api1.Entities;
using Sajuca.Innovecs.Client.Api1.Services;
using Sajuca.Innovecs.Client.Api2.Entities;
using Sajuca.Innovecs.Client.Api2.Services;
using Sajuca.Innovecs.Client.Api3.Entities;
using Sajuca.Innovecs.Client.Api3.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sajuca.Innovecs.Application.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IFirstCompanyService _firstCompanyService;
        private readonly ISecondCompanyService _secondCompanyService;
        private readonly IThirdCompanyService _thirdCompanyService;
        private readonly IMapper _mapper;

        public QuoteService(IFirstCompanyService firstCompanyService,
            ISecondCompanyService secondCompanyService,
            IThirdCompanyService thirdCompanyService,
            IMapper mapper)
        {
            _firstCompanyService = firstCompanyService;
            _secondCompanyService = secondCompanyService;
            _thirdCompanyService = thirdCompanyService;
            _mapper = mapper;
        }

        public async Task<double> GetLowestQuote(Quote quote)
        {
            List<double> quotes = new List<double>();

            var packageDimension = _mapper.Map<PackageDimension>(quote.PackageDimension);
            var tskFirstQuoteClient =  _firstCompanyService.GetQuote(quote.ContactAddress, quote.WarehouseAddress, packageDimension);

            var carton = _mapper.Map<Cartons>(quote.PackageDimension);
            var tskSecondQuoteClient = _secondCompanyService.GetQuote(quote.ContactAddress, quote.WarehouseAddress, carton);

            var package = _mapper.Map<Package>(quote.PackageDimension);
            var tskThirdQuoteClient = _thirdCompanyService.GetQuote(quote.ContactAddress, quote.WarehouseAddress, package);

            var firstQuoteResult = await tskFirstQuoteClient;
            var secondQuoteResult = await tskSecondQuoteClient;
            var thirdQuoteResult = await tskThirdQuoteClient;

            quotes.Add(firstQuoteResult.Total);
            quotes.Add(secondQuoteResult.Amount);
            quotes.Add(thirdQuoteResult.Quote);

            var result = quotes.Min();
            return result;
        }
    }
}
