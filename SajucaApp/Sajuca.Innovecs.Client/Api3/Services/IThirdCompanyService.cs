using Sajuca.Innovecs.Client.Api3.Entities;
using Sajuca.Innovecs.Client.Api3.Models;
using System.Threading.Tasks;

namespace Sajuca.Innovecs.Client.Api3.Services
{
    public interface IThirdCompanyService
    {
        Task<ThirdCompanyQuote> GetQuote(string source, string destination, Package package);
    }
}
