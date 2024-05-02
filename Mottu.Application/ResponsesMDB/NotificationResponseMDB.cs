using Mottu.Application.InterfacesMDB;
using Mottu.Domain.EntitiesMDB;

namespace Mottu.Application.ResponsesMDB
{
    public class NotificationResponseMDB : IResponseMDB
    {
        [JsonProperty(PropertyName = "notification_date")]
        public DateTime NotificationDate { get; private set; }

        [JsonProperty(PropertyName = "order")]
        //Navigation Properties
        public OrderMDB? Order { get; private set; }
    }
}
