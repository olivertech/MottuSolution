using Mottu.Domain.Entities;
using Newtonsoft.Json;

namespace Mottu.CrossCutting.Responses
{
    public class NotificationResponse
    {
        [JsonProperty(PropertyName = "notification_date")]
        public DateTime NotificationDate { get; private set; }

        [JsonProperty(PropertyName = "order")]
        //Navigation Properties
        public Order? Order { get; private set; }
    }
}
