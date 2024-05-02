using Mottu.Application.InterfacesMDB;
using Mottu.Domain.EntitiesMDB;

namespace Mottu.Application.ResponsesMDB
{
    public class DeliveredOrderResponseMDB : IResponseMDB
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "delivered_date")]
        public DateOnly? DeliveredDate { get; set; }

        [JsonProperty(PropertyName = "user")]
        public AppUserMDB? User { get; private set; }

        [JsonProperty(PropertyName = "order")]
        public OrderMDB? Order { get; private set; }
    }
}
