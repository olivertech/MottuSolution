using Mottu.CrossCutting.Requests.Base;
using Mottu.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mottu.CrossCutting.Requests
{
    public class OrderRequestUpdate : BaseRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public Guid Id { get; set; }

        [JsonPropertyName("status_order_id")]
        [JsonProperty(PropertyName = "status_order_id")]
        [Required(ErrorMessage = "O campo Id da Situação do Pedido é obrigatório")]
        public Guid? StatusOrderId { get; private set; }
    }
}
