using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
using Mottu.Application.Requests.Base;
using Mottu.Application.Responses;
using Mottu.Application.Services;
using Mottu.Domain.Entities;

namespace Mottu.Application.Interfaces
{
    public interface IBikeService : IServiceBase<Bike, BikeRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<BikeResponse>>> GetAll(BaseRequest request);
    }
}
