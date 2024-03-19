using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Mottu.Domain.Entities;

namespace Mottu.Infrastructure.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.HasOne(x => x.StatusOrder);
            builder.Property(x => x.Description).HasColumnName("Description").IsRequired(false);
            builder.Property(x => x.DateOrder).HasColumnName("Date_Order").IsRequired();
            builder.Property(x => x.ValueOrder).HasColumnName("Value_Order").IsRequired();
            builder.ToTable("Order");
        }
    }
}
