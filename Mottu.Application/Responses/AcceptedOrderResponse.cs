using Mottu.Application.Interfaces;
using Mottu.Domain.Entities;
using Newtonsoft.Json;

namespace Mottu.Application.Responses
{
    public class AcceptedOrderResponse : IResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "accepted_date")]
        public DateOnly? AcceptedDate { get; set; }

        [JsonProperty(PropertyName = "user")]
        public AppUser? User { get; set; }

        [JsonProperty(PropertyName = "order")]
        public Order? Order { get; set; }
    }
}
