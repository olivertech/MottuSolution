namespace Mottu.Application.RequestsMDB.Base
{
    public class BaseRequestMDB
    {
        [JsonPropertyName("request_user_id")]
        [JsonProperty(PropertyName = "request_user_id")]
        public string? RequestUserId { get; set; }
    }
}
