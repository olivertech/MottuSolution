using Mottu.Application.Interfaces;
using Mottu.Domain.Entities;
using Newtonsoft.Json;

namespace Mottu.Application.Responses
{
    public class NotificationResponse : IResponse
    {
        [JsonProperty(PropertyName = "notification_date")]
        public DateTime NotificationDate { get; private set; }

        [JsonProperty(PropertyName = "order")]
        //Navigation Properties
        public Order? Order { get; private set; }
    }
}
