namespace Mottu.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
