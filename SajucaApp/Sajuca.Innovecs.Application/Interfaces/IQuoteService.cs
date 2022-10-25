using Sajuca.Innovecs.Application.Models;
using System.Threading.Tasks;

namespace Sajuca.Innovecs.Application.Interfaces
{
    public interface IQuoteService
    {
        Task<double> GetLowestQuote(Quote quote);
    }
}
