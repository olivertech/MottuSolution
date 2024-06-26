﻿using Mottu.CrossCutting.Interfaces;
using Mottu.Domain.Entities;
using Newtonsoft.Json;

namespace Mottu.CrossCutting.Responses
{
    public class ListNotificatedUsersResponse : IResponse
    {
        [JsonProperty(PropertyName = "list_notificated_users")]
        public IEnumerable<AppUserResponse>? ListNotificatedUsers { get; set; }

        [JsonProperty(PropertyName = "order")]
        public OrderResponse? Order { get; set; }
    }
}
