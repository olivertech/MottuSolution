using Mottu.Application.Requests.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mottu.Application.Requests
{
    public class RentalRequestUpdate : BaseRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public Guid Id { get; set; }

        [JsonPropertyName("created_date")]
        [JsonProperty(PropertyName = "created_date")]
        [Required(ErrorMessage = "O campo Data de Criação é obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("prediction_date")]
        [JsonProperty(PropertyName = "prediction_date")]
        [Required(ErrorMessage = "O campo Data de Previsão é obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PredictionDate { get; set; }

        [JsonPropertyName("end_date")]
        [JsonProperty(PropertyName = "end_date")]
        [Required(ErrorMessage = "O campo Data de Término é obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("initial_date")]
        [JsonProperty(PropertyName = "initial_date")]
        [Required(ErrorMessage = "O campo Data de Início é obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime InitialDate { get; set; }

        [JsonPropertyName("total_value")]
        [JsonProperty(PropertyName = "total_value")]
        public double TotalValue { get; set; }

        [JsonPropertyName("num_more_dailys")]
        [JsonProperty(PropertyName = "num_more_dailys")]
        public int NumMoreDailys { get; set; }

        [JsonPropertyName("is_active")]
        [JsonProperty(PropertyName = "is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("bike_id")]
        [JsonProperty(PropertyName = "bike_id")]
        [Required(ErrorMessage = "O campo Id da Moto é obrigatório")]
        public Guid BikeId { get; set; }

        [JsonPropertyName("user_id")]
        [JsonProperty(PropertyName = "user_id")]
        [Required(ErrorMessage = "O campo Id do Usuário é obrigatório")]
        public Guid UserId { get; set; }

        [JsonPropertyName("plan_id")]
        [JsonProperty(PropertyName = "plan_id")]
        [Required(ErrorMessage = "O campo Id do Plano é obrigatório")]
        public Guid PlanId { get; set; }
    }
}
