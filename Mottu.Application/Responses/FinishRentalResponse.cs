using Mottu.Application.Interfaces;
using Mottu.Domain.Entities;

namespace Mottu.Application.Responses
{
    public class FinishRentalResponse : IResponse
    {
        public Rental? Rental { get; set; }
        public string TotalValueRental { get; set; }

        public FinishRentalResponse(Rental rental, double totalValueRental)
        {
            Rental = rental;
            TotalValueRental = String.Format("Valor total a ser pago, incluindo diárias do plano + multa : R$ {0:N}", totalValueRental);
        }
    }
}
