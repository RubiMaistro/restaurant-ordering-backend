using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.ValueObjects;

namespace Restaurant.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.OwnsOne(
                typeof(Money),
                "TotalAmount",
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

            // Aggregate boundary
            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey("OrderId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
