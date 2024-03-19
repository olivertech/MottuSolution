using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Mottu.Infrastructure.Context;
using Mottu.Infrastructure.Repositories.Base;
using System.Diagnostics.CodeAnalysis;

namespace Mottu.Infrastructure.Repositories
{
    public class BikeRepository : RepositoryBase<Bike>, IBikeRepository
    {
        public BikeRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
