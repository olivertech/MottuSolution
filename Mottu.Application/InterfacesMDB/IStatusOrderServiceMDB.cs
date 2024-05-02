using Mottu.Application.InterfacesMDB.Base;
using Mottu.Application.RequestsMDB;
using Mottu.Domain.EntitiesMDB;

namespace Mottu.Application.InterfacesMDB
{
    public interface IStatusOrderServiceMDB : IServiceBaseMDB<StatusOrderMDB, StatusOrderRequestMDB>
    {
        Task<ServiceResponseFactory<IEnumerable<StatusOrderMDB>>> GetListByName(string name);
    }
}
