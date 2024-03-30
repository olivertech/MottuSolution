namespace Mottu.Infrastructure.EntityConfiguration
{
    public class CnhTypeConfiguration : IEntityTypeConfiguration<CnhType>
    {
        public void Configure(EntityTypeBuilder<CnhType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.ToTable("Cnh_Type");

            //Seed
            builder.HasData(
                new CnhType(Guid.NewGuid(), "A"),
                new CnhType(Guid.NewGuid(), "B"),
                new CnhType(Guid.NewGuid(), "AB"),
                new CnhType(Guid.NewGuid(), "NA"));
        }
    }
}
