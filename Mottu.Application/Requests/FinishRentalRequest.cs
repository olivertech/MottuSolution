namespace Mottu.Application.Requests
{
    public class FinishRentalRequest : BaseRequest, IRequest
    {
        [JsonPropertyName("rental_id")]
        [JsonProperty(PropertyName = "rental_id")]
        [Required(ErrorMessage = "O campo Id da locação é obrigatório")]
        public Guid RentalId { get; set; }

        [JsonPropertyName("finish_rental_date")]
        [JsonProperty(PropertyName = "finish_rental_date")]
        [Required(ErrorMessage = "O campo data final da locação é obrigatório")]
        public DateOnly FinishRentalDate { get; set; }
    }
}
