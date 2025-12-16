using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;

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
