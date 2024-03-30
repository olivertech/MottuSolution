namespace Mottu.Infrastructure.EntityConfiguration
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.ToTable("User_Type");

            //Seed
            builder.HasData(
                new UserType(Guid.NewGuid(), "Administrador"),
                new UserType(Guid.NewGuid(), "Entregador"),
                new UserType(Guid.NewGuid(), "Consumidor"));
        }
    }
}
