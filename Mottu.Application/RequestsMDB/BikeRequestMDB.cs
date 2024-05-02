using Mottu.Application.RequestsMDB.Base;

namespace Mottu.Application.RequestsMDB
{
    public class BikeRequestMDB : BaseRequestMDB, IRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public string? Id { get; set; }

        [JsonPropertyName("model")]
        [JsonProperty(PropertyName = "model")]
        [Required(ErrorMessage = "O campo Modelo é obrigatório")]
        public string? Model { get; set; }

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

        private string? year;

        [JsonPropertyName("year")]
        [JsonProperty(PropertyName = "year")]
        [Required(ErrorMessage = "O campo Ano é obrigatório")]
        [StringLength(maximumLength: 4, MinimumLength = 4, ErrorMessage = "Informe ano com 4 digitos.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:9999}")]
        [AllowedValues(["2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030"], ErrorMessage = "Só são permitidas motos com ano igual ou superior a 2015")]
        public string? Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }
    }
}
