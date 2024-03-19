using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottu.Domain.Entities;

namespace Mottu.Infrastructure.EntityConfiguration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Login).HasColumnName("Login").IsRequired();
            builder.Property(x => x.Password).HasColumnName("Password").IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Cnpj).HasColumnName("CNPJ").HasMaxLength(14).IsRequired();
            builder.Property(x => x.Cnh).HasColumnName("CNH").HasMaxLength(11).IsRequired();
            builder.Property(x => x.BirthDate).HasColumnName("BirthDate").IsRequired(false);
            builder.Property(x => x.PathCnhImage).HasColumnName("Path_CNH_Image").IsRequired(false);
            builder.Property(x => x.IsActive).HasColumnName("Is_Active").IsRequired().HasDefaultValue(true);
            builder.Property(x => x.IsDelivering).HasColumnName("Is_Delivering").IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.CnhType);
            builder.HasOne(x => x.UserType);
            builder.ToTable("User");
        }
    }
}
