using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottu.Domain.Entities;

namespace Mottu.Infrastructure.EntityConfiguration
{
    public class ConsumerConfiguration : IEntityTypeConfiguration<Consumer>
    {
        public void Configure(EntityTypeBuilder<Consumer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(150);
            builder.Property(x => x.State).HasMaxLength(25);
            builder.Property(x => x.City).HasMaxLength(150);
            builder.Property(x => x.Neighborhood).HasMaxLength(150);
            builder.Property(x => x.ZipCode).HasMaxLength(8);
        }
    }
}
