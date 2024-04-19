using Microsoft.Extensions.Caching.Distributed;

namespace Mottu.Infrastructure.Repositories
{
    public class BikeRepository : RepositoryBase<Bike>, IBikeRepository
    {
        public BikeRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
