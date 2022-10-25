using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sajuca.Innovecs.Application.Interfaces;
using Sajuca.Innovecs.Application.Models;
using System.Threading.Tasks;

namespace Sajuca.Innovecs.Api.Controllers
{
    public class QuoteController : Controller
    {
        private readonly IQuoteService _quoteService;
        private readonly ILogger<QuoteController> _logger;

        public QuoteController(ILogger<QuoteController> logger, IQuoteService quoteService) 
        {
            _logger = logger;
            _quoteService = quoteService;
        }

        [HttpPost, Route("pricing")]
        public async Task<IActionResult> GetQuote([FromBody] Quote request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _quoteService.GetLowestQuote(request);
            return Ok(result);
        }       
    }
}
