namespace Mottu.Application.Requests
{
    public class AcceptedOrderRequest : IRequest
    {
        [JsonPropertyName("user_id")]
        [JsonProperty(PropertyName = "user_id")]
        [Required(ErrorMessage = "O campo Id do usuário é obrigatório")]
        public Guid UserId { get; set; }

        [JsonPropertyName("order_id")]
        [JsonProperty(PropertyName = "order_id")]
        [Required(ErrorMessage = "O campo Id do pedido é obrigatório")]
        public Guid OrderId { get; set; }
    }
}
