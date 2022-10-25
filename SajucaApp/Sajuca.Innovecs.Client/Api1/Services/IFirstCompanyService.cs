using Sajuca.Innovecs.Client.Api1.Entities;
using Sajuca.Innovecs.Client.Api1.Models;
using System.Threading.Tasks;

namespace Sajuca.Innovecs.Client.Api1.Services
{
    public interface IFirstCompanyService
    {
        Task<FirstCompanyQuote> GetQuote(string contactAddress, string warehouseAddress, PackageDimension dimensions);
    }
}
