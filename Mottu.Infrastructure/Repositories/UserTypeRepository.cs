using Microsoft.Extensions.Caching.Distributed;

namespace Mottu.Infrastructure.Repositories
{
    public class UserTypeRepository : RepositoryBaseWithRedis<UserType>, IUserTypeRepository
    {
        public UserTypeRepository([NotNull] AppDbContext context, IDistributedCache cache) : base(context, cache, "usertype")
        {
        }
    }
}
