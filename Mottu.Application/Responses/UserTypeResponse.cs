﻿namespace Mottu.Application.Responses
{
    public class UserTypeResponse : IResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
    }
}
