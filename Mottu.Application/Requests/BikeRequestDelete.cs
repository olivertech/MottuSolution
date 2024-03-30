namespace Mottu.Application.Requests
{
    public class BikeRequestDelete : BaseRequest, IRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public Guid Id { get; set; }
    }
}
