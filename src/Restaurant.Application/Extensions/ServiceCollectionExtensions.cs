using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.DTOs;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Restaurant.Application.Validators;

namespace Restaurant.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateOrderDto>, CreateOrderValidator>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
