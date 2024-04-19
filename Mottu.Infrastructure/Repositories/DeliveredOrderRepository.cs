using Microsoft.Extensions.Caching.Distributed;

namespace Mottu.Infrastructure.Repositories
{
    public class DeliveredOrderRepository : RepositoryBase<DeliveredOrder>, IDeliveredOrderRepository
    {
        public DeliveredOrderRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
