using Microsoft.Extensions.Caching.Distributed;

namespace Mottu.Infrastructure.Repositories
{
    public class AcceptedOrderRepository : RepositoryBase<AcceptedOrder>, IAcceptedOrderRepository
    {
        public AcceptedOrderRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
