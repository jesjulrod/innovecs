using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sajuca.Innovecs.Application.Models;
using Sajuca.Innovecs.Application.Utils;
using System;
using System.Threading.Tasks;

namespace Sajuca.Innovecs.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class ThirdCompanyQuoteController : Controller
    {
        [HttpPost, Route("third/quote.{format}"), FormatFilter]
        public async Task<IActionResult> GetQuote([FromBody] Quote request)
        {
            try
            {
                var dtoThirdQuote = new DtoThirdCompanyQuote() { Quote = await GeneratorRandom.GetNumberAsync(1.23, 5.34) };
                return Ok(dtoThirdQuote);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
