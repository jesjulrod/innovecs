using Sajuca.Innovecs.Client.Api2.Entities;
using Sajuca.Innovecs.Client.Api2.Models;
using System.Threading.Tasks;

namespace Sajuca.Innovecs.Client.Api2.Services
{
    public interface ISecondCompanyService
    {
        Task<SecondCompanyQuote> GetQuote(string consignor, string consignee, Cartons cartons);
    }
}
