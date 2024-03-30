namespace Mottu.Infrastructure.EntityConfiguration
{
    public class DeliveredOrderConfiguration : IEntityTypeConfiguration<DeliveredOrder>
    {
        public void Configure(EntityTypeBuilder<DeliveredOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => new { x.UserId, x.OrderId });

            builder.HasOne(nu => nu.User)
                    .WithMany(x => x.DeliveredOrders)
                    .HasForeignKey(nu => nu.UserId);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.DeliveredOrders)
                .HasForeignKey(x => x.OrderId);

            builder.Property(x => x.DeliveredDate).HasColumnName("Delivered_Date").IsRequired();
            builder.ToTable("Delivered_Orders");
        }
    }
}
