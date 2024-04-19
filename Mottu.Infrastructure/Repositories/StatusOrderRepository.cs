using Microsoft.Extensions.Caching.Distributed;

namespace Mottu.Infrastructure.Repositories
{
    public class StatusOrderRepository : RepositoryBaseWithRedis<StatusOrder>, IStatusOrderRepository
    {
        public StatusOrderRepository([NotNull] AppDbContext context, IDistributedCache cache) : base(context, cache, "statusorder")
        {
        }
    }
}
