using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain;

namespace Ordering.Infrastructure.EntityConfigurations
{
    public sealed class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders", OrderingContext.DEFAULT_SCHEMA);
            builder.HasKey(o => o.Id);
            builder.Ignore(o => o.DomainEvents);
            builder.Property(o => o.CustomerId).IsRequired(true);
            builder.Property(o => o.CreatedAt).IsRequired(true);
            builder.Property(o => o.Status).IsRequired(true);
        }
    }
}
