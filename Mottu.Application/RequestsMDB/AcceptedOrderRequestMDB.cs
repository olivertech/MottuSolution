namespace Mottu.Application.RequestsMDB
{
    public class AcceptedOrderRequestMDB : IRequest
    {
        [JsonPropertyName("user_id")]
        [JsonProperty(PropertyName = "user_id")]
        [Required(ErrorMessage = "O campo Id do usuário é obrigatório")]
        public string? UserId { get; set; }

        [JsonPropertyName("order_id")]
        [JsonProperty(PropertyName = "order_id")]
        [Required(ErrorMessage = "O campo Id do pedido é obrigatório")]
        public string? OrderId { get; set; }
    }
}
