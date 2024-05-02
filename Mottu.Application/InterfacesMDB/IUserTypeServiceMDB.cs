using Mottu.Application.InterfacesMDB.Base;
using Mottu.Application.RequestsMDB;
using Mottu.Domain.EntitiesMDB;

namespace Mottu.Application.InterfacesMDB
{
    public interface IUserTypeServiceMDB : IServiceBaseMDB<UserTypeMDB, UserTypeRequestMDB>
    {
        Task<ServiceResponseFactory<IEnumerable<UserTypeMDB>>> GetListByName(string name);
    }
}
