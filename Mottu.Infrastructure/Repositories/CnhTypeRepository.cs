using Microsoft.Extensions.Caching.Distributed;

namespace Mottu.Infrastructure.Repositories
{
    public class CnhTypeRepository : RepositoryBaseWithRedis<CnhType>, ICnhTypeRepository
    {
        public CnhTypeRepository([NotNull] AppDbContext context, IDistributedCache cache) : base(context, cache, "cnhtype")
        {
        }
    }
}
