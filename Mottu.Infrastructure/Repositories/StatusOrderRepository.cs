using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Mottu.Infrastructure.Context;
using Mottu.Infrastructure.Repositories.Base;
using System.Diagnostics.CodeAnalysis;

namespace Mottu.Infrastructure.Repositories
{
    public class StatusOrderRepository : RepositoryBase<StatusOrder>, IStatusOrderRepository
    {
        public StatusOrderRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
