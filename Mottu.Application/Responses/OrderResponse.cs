using Mottu.Application.Interfaces;
using Mottu.Domain.Entities;
using Newtonsoft.Json;

namespace Mottu.Application.Responses
{
    public class OrderResponse : IResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; }

        [JsonProperty(PropertyName = "date_order")]
        public DateOnly DateOrder { get; private set; }

        [JsonProperty(PropertyName = "value_order")]
        public double ValueOrder { get; private set; }

        [JsonProperty(PropertyName = "status_order")]
        public StatusOrder? StatusOrder { get; set; }
    }
}
