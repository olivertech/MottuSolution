﻿namespace Mottu.Application.Responses
{
    public class DeliveredOrderResponse : IResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "delivered_date")]
        public DateOnly? DeliveredDate { get; set; }

        [JsonProperty(PropertyName = "user")]
        public AppUser? User { get; private set; }

        [JsonProperty(PropertyName = "order")]
        public Order? Order { get; private set; }
    }
}
