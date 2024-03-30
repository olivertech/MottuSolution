namespace Mottu.Infrastructure.EntityConfiguration
{
    public class NotificatedUserConfiguration : IEntityTypeConfiguration<NotificatedUser>
    {
        public void Configure(EntityTypeBuilder<NotificatedUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => new { x.UserId, x.NotificationId});

            builder.HasOne(nu => nu.User)
                    .WithMany(x => x.NotificatedUsers)
                    .HasForeignKey(nu => nu.UserId);

            builder.HasOne(x => x.Notification)
                .WithMany(x => x.NotificatedUsers)
                .HasForeignKey(x => x.NotificationId);

            builder.Property(x => x.NotificationDate).HasColumnName("Notification_Date").IsRequired();
            builder.ToTable("Notificated_User");
        }
    }
}
