using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces.Base;

namespace Mottu.Domain.Interfaces
{
    public interface IAppUserRepository : IRepositoryBase<AppUser>
    {
        Task<IEnumerable<AppUser>?> GetFullAll();
        Task<AppUser?> GetFullById(Guid? id);
        Task<IEnumerable<AppUser>?> GetFullList(string name);
        Task<IEnumerable<AppUser>?> GetFullListOfNotificatedUsers(Guid orderId);
    }
}
