using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Infrastructure.Order;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.CustomerId).IsRequired();
        builder.Property(o => o.Status).HasConversion<int>().IsRequired();

        // TODO: this one needs thinking: builder.Property(o => o.ProductIds).IsRequired();

        builder.Property(o => o.ShippingAddress).HasMaxLength(512).IsRequired();
        builder.Property(o => o.PaymentMethod).HasMaxLength(128).IsRequired();
        builder.Property(o => o.OptionalNote).HasMaxLength(1024).IsRequired(false);
        builder.Property(o => o.PlacedAt).IsRequired();
    }
}