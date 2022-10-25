using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sajuca.Innovecs.Client.Api1.Services;
using Sajuca.Innovecs.Client.Api2.Services;
using Sajuca.Innovecs.Client.Api3.Services;

namespace Sajuca.Innovecs.Client.Extensions
{
    public static class ClientExtension
    {
        public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddScoped<IFirstCompanyService, FirstCompanyService>();
            services.AddScoped<ISecondCompanyService, SecondCompanyService>();
            services.AddScoped<IThirdCompanyService, ThirdCompanyService>();

            return services;
        }
    }
}
