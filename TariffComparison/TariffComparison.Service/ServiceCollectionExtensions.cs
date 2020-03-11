using Microsoft.Extensions.DependencyInjection;
using TariffComparison.Service.DataAccess;

namespace TariffComparison.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTariffComparison( this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductCostCalculator, ProductCostCalculator>();
            services.AddTransient<IProductComparisonService, ProductComparisonService>();
        }
    }
}
