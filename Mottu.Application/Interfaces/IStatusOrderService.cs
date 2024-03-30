namespace Mottu.Application.Interfaces
{
    public interface IStatusOrderService : IServiceBase<StatusOrder, StatusOrderRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<StatusOrder>>> GetListByName(string name);
    }
}
