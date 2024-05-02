using Mottu.Application.InterfacesMDB;
using Mottu.Domain.EntitiesMDB;

namespace Mottu.Application.ResponsesMDB
{
    public class OrderResponseMDB : IResponseMDB
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; }

        [JsonProperty(PropertyName = "date_order")]
        public DateOnly DateOrder { get; private set; }

        [JsonProperty(PropertyName = "value_order")]
        public double ValueOrder { get; private set; }

        [JsonProperty(PropertyName = "status_order")]
        public StatusOrderMDB? StatusOrder { get; set; }
    }
}
