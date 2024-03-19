using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Mottu.Domain.Entities;

namespace Mottu.Infrastructure.EntityConfiguration
{
    public class AppAdministratorConfiguration : IEntityTypeConfiguration<AppAdministrator>
    {
        public void Configure(EntityTypeBuilder<AppAdministrator> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Login).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Cnpj).HasMaxLength(14).IsRequired();
            builder.Property(x => x.BirthDate);
        }
    }
}
