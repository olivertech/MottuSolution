﻿using Mottu.Application.Interfaces;
using Newtonsoft.Json;

namespace Mottu.Application.Responses
{
    public class CnhTypeResponse : IResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
    }
}
