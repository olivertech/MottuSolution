using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Mottu.Domain.Repositories.Base;

namespace Mottu.Domain.Repositories
{
    public class OrderRepository : RepositoryBase<Order> , IOrderRepository
    {
    }
}
