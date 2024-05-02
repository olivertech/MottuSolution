using Mottu.Application.InterfacesMDB.Base;

namespace Mottu.Application.InterfacesMDB
{
    public interface IBikeServiceMDB : IServiceBaseMDB<Bike, BikeRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<Bike>>> GetListByPlate(BikeByPlateRequest request);
    }
}
