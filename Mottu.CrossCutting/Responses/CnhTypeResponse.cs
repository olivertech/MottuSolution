using Mottu.CrossCutting.Interfaces;
using Newtonsoft.Json;

namespace Mottu.CrossCutting.Responses
{
    public class CnhTypeResponse : IResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
    }
}
