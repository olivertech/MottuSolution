using Mottu.Application.RequestsMDB.Base;

namespace Mottu.Application.RequestsMDB
{
    public class BikeRequestDeleteMDB : BaseRequestMDB, IRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public string? Id { get; set; }
    }
}
