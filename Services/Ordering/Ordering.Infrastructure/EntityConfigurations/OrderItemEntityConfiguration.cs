using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain;

namespace Ordering.Infrastructure.EntityConfigurations
{
    public sealed class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("orderItems", OrderingContext.DEFAULT_SCHEMA);
            builder.HasKey(o => o.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property<int>("OrderId").IsRequired();
            builder.Property(o => o.Description).IsRequired(true);
            builder.Property(o => o.Quantity).IsRequired(true);
            builder.Property(o => o.Price).IsRequired(true);
        }
    }
}
