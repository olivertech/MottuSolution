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
                new UserType(Guid.Parse("f6a2372a-b146-45f9-be70-a0be13736dd8"), "Administrador"),
                new UserType(Guid.NewGuid(), "Entregador"),
                new UserType(Guid.NewGuid(), "Consumidor"));
        }
    }
}
