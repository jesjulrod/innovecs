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
    public class SecondCompanyQuoteController : Controller
    {
        [HttpPost, Route("second/quote")]
        public async Task<IActionResult> GetQuote([FromBody] Quote request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var dtoSecondQuote = new DtoSecondCompanyQuote() { Amount = await GeneratorRandom.GetNumberAsync(1.23, 5.34) };
                return Ok(dtoSecondQuote);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
