using Mottu.Application.InterfacesMDB.Base;

namespace Mottu.Application.InterfacesMDB
{
    public interface IRentalServiceMDB : IServiceBaseMDB<Rental, RentalRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<Rental>>> GetListByBike(Guid bikeId);

        ServiceResponseFactory<FinishRentalResponse> FinishRental(FinishRentalRequest request);
    }
}
