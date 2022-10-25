using System;
using System.Threading.Tasks;

namespace Sajuca.Innovecs.Application.Utils
{
    public static class GeneratorRandom
    {
        public static async Task<double> GetNumberAsync(double minimum, double maximum)
        {
            return await Task.Run(() =>
            {
                var random = new Random();
                return random.NextDouble() * (maximum - minimum) + minimum; ;
            });
        }
    }
}
