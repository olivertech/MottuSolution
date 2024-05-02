namespace Mottu.Application.ResponsesMDB
{
    public class ListNotificatedUsersResponseMDB : IResponse
    {
        [JsonProperty(PropertyName = "list_notificated_users")]
        public IEnumerable<AppUserResponseMDB>? ListNotificatedUsers { get; set; }

        [JsonProperty(PropertyName = "order")]
        public OrderResponseMDB? Order { get; set; }
    }
}
