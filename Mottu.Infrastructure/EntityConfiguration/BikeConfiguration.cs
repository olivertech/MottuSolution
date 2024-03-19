using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Mottu.Domain.Entities;

namespace Mottu.Infrastructure.EntityConfiguration
{
    public class BikeConfiguration : IEntityTypeConfiguration<Bike>
    {
        public void Configure(EntityTypeBuilder<Bike> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Plate).HasDatabaseName("Plate").IsUnique();
            builder.Property(x => x.Plate).IsRequired();
            builder.Property(x => x.Model).HasColumnName("Model").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Year).HasColumnName("Year").IsRequired();
            builder.ToTable("Bike");
        }
    }
}
