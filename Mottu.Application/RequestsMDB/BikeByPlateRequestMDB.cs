using Mottu.Application.RequestsMDB.Base;

namespace Mottu.Application.RequestsMDB
{
    public class BikeByPlateRequestMDB : BaseRequestMDB, IRequest
    {
        private string? plate;

        [JsonPropertyName("plate")]
        [JsonProperty(PropertyName = "plate")]
        [Required(ErrorMessage = "O campo Placa é obrigatório")]
        [StringLength(7, ErrorMessage = "Informe placa com 7 caracteres.")]
        public string? Plate
        {
            get
            {
                return plate!.ToUpper();
            }
            set
            {
                plate = value!.ToUpper();
            }
        }
    }
}
