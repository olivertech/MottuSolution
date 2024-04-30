using Microsoft.Extensions.Caching.Distributed;

namespace Mottu.Infrastructure.Repositories
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        public AppUserRepository([NotNull] AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AppUser>?> GetFullAll()
        {
            return await _context!.Users
                .Include(ut => ut.UserType)
                .Include(ct => ct.CnhType)
                .ToListAsync();
        }

        public async Task<AppUser?> GetFullById(Guid? id)
        {
            return await _context!.Users
                .Include(ut => ut.UserType)
                .Include(ct => ct.CnhType)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>?> GetFullList(string name)
        {
            return await _context!.Users
                .Include(ut => ut.UserType)
                .Include(ct => ct.CnhType)
                .Where(x => x.Name!.ToLower() == name.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<AppUser>?> GetFullListOfNotificatedUsers(Guid orderId)
        {
            var notificationList = await _context!.NotificatedUsers.Where(x => x.Notification!.Order!.Id == orderId).ToListAsync();
            List<AppUser> usersList = [];

            foreach (var notification in notificationList)
            {
                var user = _context!.Users
                            .Include(ut => ut.UserType)
                            .Include(ct => ct.CnhType)
                            .Where(x => x.Id == notification.UserId).FirstOrDefault();

                usersList.Add(user!);
            }

            return usersList;
        }
    }
}
