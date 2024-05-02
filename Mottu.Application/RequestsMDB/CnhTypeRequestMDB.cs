using Mottu.Application.RequestsMDB.Base;

namespace Mottu.Application.RequestsMDB
{
    public class CnhTypeRequestMDB : BaseRequestMDB, IRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        [JsonProperty(PropertyName = "name")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Informe um nome que tenha mínimo de 3 e máximo de 100 caracteres.")]
        public string? Name { get; set; }
    }
}
