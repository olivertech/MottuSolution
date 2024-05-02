using Mottu.Application.InterfacesMDB;
using Mottu.Domain.EntitiesMDB;

namespace Mottu.Application.ResponsesMDB
{
    public class NotificatedUserResponseMDB : IResponseMDB
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "notification_date")]
        public DateOnly? NotificationDate { get; set; }

        [JsonProperty(PropertyName = "notification")]
        public NotificationMDB? Notification { get; set; }

        [JsonProperty(PropertyName = "user")]
        public AppUserMDB? User { get; private set; }
    }
}
