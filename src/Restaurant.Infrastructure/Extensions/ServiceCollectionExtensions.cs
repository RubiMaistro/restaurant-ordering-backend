using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Infrastructure.Persistence;

namespace Restaurant.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {

            services.AddRepositories(configuration);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("RestaurantDb");
            });

            return services;

        }
    }
}
