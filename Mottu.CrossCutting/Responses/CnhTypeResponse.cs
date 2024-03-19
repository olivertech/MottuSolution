using Newtonsoft.Json;

namespace Mottu.CrossCutting.Responses
{
    public class CnhTypeResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
    }
}
