using Mottu.Domain.EntitiesMDB;

namespace Mottu.Application.ResponsesMDB
{
    public class FinishRentalResponseMDB : IResponse
    {
        public RentalMDB? Rental { get; set; }
        public string TotalValueRental { get; set; }

        public FinishRentalResponseMDB(RentalMDB rental, double totalValueRental)
        {
            Rental = rental;
            TotalValueRental = String.Format("Valor total a ser pago, incluindo diárias do plano + multa : R$ {0:N}", totalValueRental);
        }
    }
}
