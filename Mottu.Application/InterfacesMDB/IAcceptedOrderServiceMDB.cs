using Mottu.Application.InterfacesMDB.Base;

namespace Mottu.Application.InterfacesMDB
{
    public interface IAcceptedOrderServiceMDB : IServiceBaseMDB<AcceptedOrder, AcceptedOrderRequest>
    {
        Task<ServiceResponseFactory<AcceptedOrderResponse>> AcceptOrder(AcceptedOrderRequest request);
    }
}
