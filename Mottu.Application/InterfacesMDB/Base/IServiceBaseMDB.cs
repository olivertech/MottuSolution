namespace Mottu.Application.InterfacesMDB.Base
{
    public interface IServiceBaseMDB<E, R>
        where E : IEntity
        where R : IRequestMDB
    {
        Task<ServiceResponseFactory<IEnumerable<E>>> GetAll();
        Task<ServiceResponseFactory<E>> GetById(Guid id);
        Task<ServiceResponseFactory<long>> GetCount();
        Task<ServiceResponseFactory<E>> Insert(R request);
        Task<ServiceResponseFactory<E>> Update(R request);
        Task<ServiceResponseFactory<E>> Delete(R request);
    }
}
