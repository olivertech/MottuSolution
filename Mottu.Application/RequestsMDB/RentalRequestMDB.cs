using Mottu.Application.RequestsMDB.Base;

namespace Mottu.Application.RequestsMDB
{
    public class RentalRequestMDB : BaseRequestMDB, IRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public string? Id { get; set; }

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
