using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Application.Services;
using Mottu.Domain.Entities;

namespace Mottu.Application.Interfaces
{
    public interface IAcceptedOrderService : IServiceBase<AcceptedOrder, AcceptedOrderRequest>
    {
        Task<ServiceResponseFactory<AcceptedOrderResponse>> AcceptOrder(AcceptedOrderRequest request);
    }
}
