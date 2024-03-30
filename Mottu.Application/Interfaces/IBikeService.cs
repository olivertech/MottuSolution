namespace Mottu.Application.Interfaces
{
    public interface IBikeService : IServiceBase<Bike, BikeRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<BikeResponse>>> GetAll(BaseRequest request);
    }
}
