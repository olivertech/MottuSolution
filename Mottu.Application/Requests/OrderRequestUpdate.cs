namespace Mottu.Application.Requests
{
    public class OrderRequestUpdate : BaseRequest, IRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public Guid Id { get; set; }

        [JsonPropertyName("status_order_id")]
        [JsonProperty(PropertyName = "status_order_id")]
        [Required(ErrorMessage = "O campo Id da Situação do Pedido é obrigatório")]
        public Guid? StatusOrderId { get; private set; }
    }
}
