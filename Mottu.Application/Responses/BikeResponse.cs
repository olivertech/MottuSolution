using Mottu.Application.Interfaces;
using Newtonsoft.Json;

namespace Mottu.Application.Responses
{
    public class BikeResponse : IResponse
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
