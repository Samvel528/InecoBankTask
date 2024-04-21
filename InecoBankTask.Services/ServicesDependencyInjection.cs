using InecoBankTask.Services.Implementations;
using InecoBankTask.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InecoBankTask.Services
{
    public static class ServicesDependencyInjection
    {
        public static IServiceCollection AddServicesLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IXMLService, XMLService>();
            services.AddTransient<IProductService, ProductInfoService>();
            services.AddTransient<IDiscountConfigurationService, DiscountConfigurationService>();
            services.AddTransient<IDiscountService, DiscountService>();

            services.AddHostedService<MyHostedService>();

            return services;
        }
    }
}
