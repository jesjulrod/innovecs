using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sajuca.Innovecs.Application.Auth;
using Sajuca.Innovecs.Application.Interfaces;
using Sajuca.Innovecs.Application.Services;
using Sajuca.Innovecs.Client.Extensions;
using System.Reflection;

namespace Sajuca.Innovecs.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IQuoteService, QuoteService>();

            services.AddClient(configuration);
            return services;
        }
    }
}
