namespace Mottu.Infrastructure.EntityConfiguration
{
    public class AcceptedOrderConfiguration : IEntityTypeConfiguration<AcceptedOrder>
    {
        public void Configure(EntityTypeBuilder<AcceptedOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => new { x.UserId, x.OrderId });

            builder.HasOne(nu => nu.User)
                    .WithMany(x => x.AcceptedOrders)
                    .HasForeignKey(nu => nu.UserId);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.AcceptedOrders)
                .HasForeignKey(x => x.OrderId);

            builder.Property(x => x.AcceptedDate).HasColumnName("Accepted_Date").IsRequired();
            builder.ToTable("Accepted_Orders");
        }
    }
}
