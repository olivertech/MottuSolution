using Microsoft.Extensions.Caching.Distributed;

namespace Mottu.Infrastructure.Repositories
{
    public class NotificatedUserRepository : RepositoryBase<NotificatedUser>, INotificatedUserRepository
    {
        public NotificatedUserRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
