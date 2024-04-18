namespace Mottu.Application.Helpers
{
    public class CalculateTotalRental
    {
        public CalculateTotalRental() 
        {
        }

        public static double GetRentalValue(Rental rental)
        {
            try
            {
                return rental.Plan.DailyValue * rental.Plan.NumDays;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static double GetFineValue(ref Rental rental, FinishRentalRequest request, IConfiguration configuration)
        {
            try
            {
                double fine = 0d;
                int daysDifference;

                //Se a data de previsão de término for maior que a data informada
                if (rental.PredictionDate.CompareTo(request.FinishRentalDate) > 0)
                {
                    daysDifference = rental.PredictionDate.DayOfYear - request.FinishRentalDate.DayOfYear;

                    switch (rental.Plan.NumDays)
                    {
                        case 7:
                            fine = (daysDifference * rental.Plan.DailyValue) * 0.2;
                            break;
                        case 15:
                            fine = (daysDifference * rental.Plan.DailyValue) * 0.4;
                            break;
                        case 30:
                            fine = (daysDifference * rental.Plan.DailyValue) * 0.6;
                            break;
                        default:
                            break;
                    }

                    rental.NumMoreDailys = -daysDifference;
                }
                else
                {
                    //Se a data de previsão de término for menor que a data informada
                    if (rental.PredictionDate.CompareTo(request.FinishRentalDate) < 0)
                    {
                        daysDifference = Math.Abs(rental.PredictionDate.DayOfYear - request.FinishRentalDate.DayOfYear);
                        _ = short.TryParse(configuration!.GetSection("AditionalTaxValue").Value!, out short aditionalTax);

                        fine = daysDifference * aditionalTax;
                        rental.NumMoreDailys = daysDifference;
                    }
                }

                return fine;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
