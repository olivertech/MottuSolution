namespace Mottu.Application.Interfaces
{
    public interface IBikeService : IServiceBase<Bike, BikeRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<Bike>>> GetListByPlate(BikeByPlateRequest request);
    }
}
