using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Api.IntegrationTests.Fixtures;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.ValueObjects;
using Restaurant.Infrastructure.Persistence;

namespace Restaurant.Api.IntergrationTests.Fixtures
{
    public class IntegrationTestFixture : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("IntegrationTest");

            builder.ConfigureServices(services =>
            {
                // original DbContextOptions registration removal
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null) 
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForIntegrationTesting");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                var newOrder = new Order
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = DateTime.UtcNow
                };

                newOrder.AddItem(new OrderItem(TestIds.OrderItemId1, 2, new Money(10, Currency.HUF)));
                newOrder.AddItem(new OrderItem(TestIds.OrderItemId2, 1, new Money(20, Currency.HUF)));


                db.Orders.Add(newOrder);

                Console.WriteLine($"Dishes count: {db.Orders.Count()}");

                db.SaveChanges();
            });
        }
    }

}
