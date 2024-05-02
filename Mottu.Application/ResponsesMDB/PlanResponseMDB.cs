using Mottu.Application.InterfacesMDB;

namespace Mottu.Application.ResponsesMDB
{
    public class PlanResponseMDB : IResponseMDB
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; }

        [JsonProperty(PropertyName = "num_days")]
        public int NumDays { get; private set; }

        [JsonProperty(PropertyName = "daily_value")]
        public double DailyValue { get; private set; }

        [JsonProperty(PropertyName = "fine_percentage")]
        public int FinePercentage { get; private set; }
    }
}
