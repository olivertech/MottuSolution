using Newtonsoft.Json;

namespace Mottu.CrossCutting.Responses
{
    public class BikeResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string? Model { get; set; }

        [JsonProperty(PropertyName = "plate")]
        public string? Plate { get; set; }

        [JsonProperty(PropertyName = "year")]
        public string? Year { get; set; }
    }
}
