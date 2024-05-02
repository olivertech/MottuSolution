using Mottu.Application.InterfacesMDB;

namespace Mottu.Application.ResponsesMDB
{
    public class StatusOrderResponseMDB : IResponseMDB
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
    }
}
