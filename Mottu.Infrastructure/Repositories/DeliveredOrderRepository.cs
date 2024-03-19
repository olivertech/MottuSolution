using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Mottu.Infrastructure.Context;
using Mottu.Infrastructure.Repositories.Base;
using System.Diagnostics.CodeAnalysis;

namespace Mottu.Infrastructure.Repositories
{
    public class DeliveredOrderRepository : RepositoryBase<DeliveredOrder>, IDeliveredOrderRepository
    {
        public DeliveredOrderRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
