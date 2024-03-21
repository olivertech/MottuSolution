using Mottu.Application.Interfaces;
using Mottu.Domain.Entities;
using Newtonsoft.Json;

namespace Mottu.Application.Responses
{
    public class NotificatedUserResponse : IResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "notification_date")]
        public DateOnly? NotificationDate { get; set; }

        [JsonProperty(PropertyName = "notification")]
        public Notification? Notification { get; set; }

        [JsonProperty(PropertyName = "user")]
        public AppUser? User { get; private set; }
    }
}
