namespace Mottu.Infrastructure.Repositories
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
