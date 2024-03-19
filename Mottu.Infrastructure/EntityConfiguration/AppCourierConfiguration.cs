using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottu.Domain.Entities;

namespace Mottu.Infrastructure.EntityConfiguration
{
    public class AppCourierConfiguration : IEntityTypeConfiguration<AppCourier>
    {
        public void Configure(EntityTypeBuilder<AppCourier> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Login).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.CnhType).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Cnpj).HasMaxLength(14).IsRequired();
            builder.Property(x => x.BirthDate);
            builder.Property(x => x.Cnh).HasMaxLength(11).IsRequired();
            builder.Property(x => x.PathCnhImage);
        }
    }
}
