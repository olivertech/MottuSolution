using Mottu.Application.Requests.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mottu.Application.Requests
{
    public class StatusOrderRequest : BaseRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        [JsonProperty(PropertyName = "name")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Informe um nome que tenha mínimo de 3 e máximo de 100 caracteres.")]
        public string? Name { get; set; }
    }
}
