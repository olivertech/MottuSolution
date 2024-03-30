namespace Mottu.Application.Responses
{
    public class StatusOrderResponse : IResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
    }
}
