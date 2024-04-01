namespace Mottu.Application.Interfaces
{
    public interface IRentalService : IServiceBase<Rental, RentalRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<Rental>>> GetListByBike(Guid bikeId);

        ServiceResponseFactory<FinishRentalResponse> FinishRental(FinishRentalRequest request);
    }
}
