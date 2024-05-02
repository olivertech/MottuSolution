using Mottu.Application.InterfacesMDB.Base;

namespace Mottu.Application.InterfacesMDB
{
    public interface ICnhTypeServiceMDB : IServiceBaseMDB<CnhType, CnhTypeRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<CnhType>>> GetListByName(string name);
    }
}
