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
    public class FirstQuoteCompanyController : Controller
    {
        [HttpPost, Route("first/quote")]
        public async Task<IActionResult> GetQuote([FromBody] Quote request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var dtoFirstQuote = new DtoFirstCompanyQuote() { Total = await GeneratorRandom.GetNumberAsync(1.23, 5.34) };
                return Ok(dtoFirstQuote);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
