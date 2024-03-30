namespace Mottu.Infrastructure.EntityConfiguration
{
    public class StatusOrderConfiguration : IEntityTypeConfiguration<StatusOrder>
    {
        public void Configure(EntityTypeBuilder<StatusOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.ToTable("Status_Order");

            //Seed
            builder.HasData(
                new StatusOrder(Guid.NewGuid(), "Disponível"),
                new StatusOrder(Guid.NewGuid(), "Aceito"),
                new StatusOrder(Guid.NewGuid(), "Entregue"));
        }
    }
}
