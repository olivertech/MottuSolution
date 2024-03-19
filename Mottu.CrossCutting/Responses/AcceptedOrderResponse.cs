using Mottu.Domain.Entities;
using Newtonsoft.Json;

namespace Mottu.CrossCutting.Responses
{
    public class AcceptedOrderResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "accepted_date")]
        public DateOnly? AcceptedDate { get; set; }

        [JsonProperty(PropertyName = "user")]
        public AppUser? User { get; private set; }

        [JsonProperty(PropertyName = "order")]
        public Order? Order { get; private set; }
    }
}
