using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mottu.CrossCutting.Requests.Base
{
    public class BaseRequest
    {
        [JsonPropertyName("request_user_id")]
        [JsonProperty(PropertyName = "request_user_id")]
        public Guid? RequestUserId { get; set; }
    }
}
