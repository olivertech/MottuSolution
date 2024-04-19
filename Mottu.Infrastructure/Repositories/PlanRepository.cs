using Microsoft.Extensions.Caching.Distributed;

namespace Mottu.Infrastructure.Repositories
{
    public class PlanRepository : RepositoryBase<Plan>, IPlanRepository
    {
        public PlanRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
