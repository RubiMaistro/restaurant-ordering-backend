using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.ValueObjects;

namespace Restaurant.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.DishId)
                .IsRequired();
            builder.Property(oi => oi.Quantity)
                .IsRequired();
            builder.OwnsOne(
                typeof(Money), 
                "UnitPrice", 
                money =>
                {
                    money.Property<decimal>(nameof(Money.Amount))
                        .HasConversion<decimal>()
                        .HasPrecision(18, 2)
                        .IsRequired();

                    money.Property<Currency>(nameof(Money.Currency))
                        .HasConversion<string>()
                        .HasMaxLength(3)
                        .IsRequired();
                });
            // Optimistic concurrency
            builder.Property<byte[]>("RowVersion")
                   .IsRowVersion();
        }
    }
}
