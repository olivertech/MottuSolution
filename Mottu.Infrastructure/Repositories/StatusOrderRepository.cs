namespace Mottu.Infrastructure.Repositories
{
    public class StatusOrderRepository : RepositoryBase<StatusOrder>, IStatusOrderRepository
    {
        public StatusOrderRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
