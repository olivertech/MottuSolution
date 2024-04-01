namespace Mottu.Application.Interfaces
{
    public interface IAcceptedOrderService : IServiceBase<AcceptedOrder, AcceptedOrderRequest>
    {
        Task<ServiceResponseFactory<AcceptedOrderResponse>> AcceptOrder(AcceptedOrderRequest request);
    }
}
