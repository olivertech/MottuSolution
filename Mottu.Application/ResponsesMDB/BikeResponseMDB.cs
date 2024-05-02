using Mottu.Application.InterfacesMDB;

namespace Mottu.Application.ResponsesMDB
{
    public class BikeResponseMDB : IResponseMDB
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string? Model { get; set; }

        [JsonProperty(PropertyName = "plate")]
        public string? Plate { get; set; }

        [JsonProperty(PropertyName = "year")]
        public string? Year { get; set; }
    }
}
