﻿namespace Mottu.Application.Requests
{
    public class NotificatedUserRequest : IRequest
    {
        [JsonPropertyName("request_user_id")]
        [JsonProperty(PropertyName = "request_user_id")]
        public Guid? RequestUserId { get; set; }

        [JsonPropertyName("order_id")]
        [JsonProperty(PropertyName = "order_id")]
        [Required(ErrorMessage = "O campo Id do Pedido é obrigatório")]
        public Guid OrderId { get; set; }
    }
}
