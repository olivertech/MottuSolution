using Mottu.Domain.Entities;
using Newtonsoft.Json;

namespace Mottu.CrossCutting.Responses
{
    public class RentalResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "created_date")]
        public DateOnly CreationDate { get; private set; }

        [JsonProperty(PropertyName = "prediction_date")]
        public DateOnly PredictionDate { get; private set; }

        [JsonProperty(PropertyName = "end_date")]
        public DateOnly EndDate { get; private set; }

        [JsonProperty(PropertyName = "initial_date")]
        public DateOnly InitialDate { get; private set; }

        [JsonProperty(PropertyName = "total_value")]
        public double TotalValue { get; private set; }

        [JsonProperty(PropertyName = "num_more_dailys")]
        public int NumMoreDailys { get; private set; }

        [JsonProperty(PropertyName = "is_active")]
        public bool IsActive { get; private set; }

        [JsonProperty(PropertyName = "bike")]
        public Bike? Bike { get; private set; }

        [JsonProperty(PropertyName = "user")]
        public AppUser? User { get; set; }

        [JsonProperty(PropertyName = "plan")]
        public Plan? Plan { get; set; }
    }
}
