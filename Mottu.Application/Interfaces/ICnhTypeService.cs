namespace Mottu.Application.Interfaces
{
    public interface ICnhTypeService : IServiceBase<CnhType, CnhTypeRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<CnhType>>> GetListByName(string name);
    }
}
