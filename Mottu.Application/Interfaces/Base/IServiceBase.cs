namespace Mottu.Application.Interfaces.Base
{
    public interface IServiceBase<E, R>
        where E : IEntity
        where R : IRequest
    {
        Task<ServiceResponseFactory<IEnumerable<E>>> GetAll();
        Task<ServiceResponseFactory<E>> GetById(Guid id);
        Task<ServiceResponseFactory<int>> GetCount();
        Task<ServiceResponseFactory<E>> Insert(R request);
        Task<ServiceResponseFactory<E>> Update(R request);
        Task<ServiceResponseFactory<E>> Delete(R request);
    }
}
