using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Mottu.Application.Requests.Base
{
    public class BaseRequest
    {
        [JsonPropertyName("request_user_id")]
        [JsonProperty(PropertyName = "request_user_id")]
        public Guid? RequestUserId { get; set; }
    }
}
