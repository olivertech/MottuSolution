﻿namespace Mottu.Application.Responses
{
    public class ListNotificatedUsersResponse : IResponse
    {
        [JsonProperty(PropertyName = "list_notificated_users")]
        public IEnumerable<AppUserResponse>? ListNotificatedUsers { get; set; }

        [JsonProperty(PropertyName = "order")]
        public OrderResponse? Order { get; set; }
    }
}
