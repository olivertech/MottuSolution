namespace Mottu.Application.Requests
{
    public class OrderRequest : BaseRequest, IRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public Guid Id { get; set; }

        [JsonPropertyName("description")]
        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; } = "12345";

        [JsonPropertyName("date_order")]
        [JsonProperty(PropertyName = "date_order")]
        [Required(ErrorMessage = "O campo Data de Criação do Pedido é obrigatório")]
        public DateOnly DateOrder { get; private set; }

        [JsonPropertyName("value_order")]
        [JsonProperty(PropertyName = "value_order")]
        [Required(ErrorMessage = "O campo Valor do Pedido é obrigatório")]
        public double ValueOrder { get; set; } = 12345;

        [JsonPropertyName("status_order_id")]
        [JsonProperty(PropertyName = "status_order_id")]
        public Guid? StatusOrderId { get; private set; }
    }
}
