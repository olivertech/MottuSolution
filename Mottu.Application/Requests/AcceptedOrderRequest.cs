using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mottu.Application.Requests
{
    public class AcceptedOrderRequest
    {
        [JsonPropertyName("user_id")]
        [JsonProperty(PropertyName = "user_id")]
        [Required(ErrorMessage = "O campo Id do usuário é obrigatório")]
        public Guid UserId { get; set; }

        [JsonPropertyName("order_id")]
        [JsonProperty(PropertyName = "order_id")]
        [Required(ErrorMessage = "O campo Id do pedido é obrigatório")]
        public Guid OrderId { get; set; }
    }
}
