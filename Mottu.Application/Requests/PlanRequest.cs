namespace Mottu.Application.Requests
{
    public class PlanRequest : BaseRequest, IRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        [JsonProperty(PropertyName = "name")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Informe um nome que tenha mínimo de 3 e máximo de 100 caracteres.")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        [JsonProperty(PropertyName = "description")]
        [StringLength(500, ErrorMessage = "Informe uma descrição que tenha máximo de 500 caracteres.")]
        public string? Description { get; set; }

        [JsonPropertyName("num_days")]
        [JsonProperty(PropertyName = "num_days")]
        [Required(ErrorMessage = "O campo Número de Dias é obrigatório")]
        public int NumDays { get; private set; }

        [JsonPropertyName("daily_value")]
        [JsonProperty(PropertyName = "daily_value")]
        [Required(ErrorMessage = "O campo Valor Diária é obrigatório")]
        public double DailyValue { get; private set; }

        [JsonPropertyName("fine_percentage")]
        [JsonProperty(PropertyName = "fine_percentage")]
        [Required(ErrorMessage = "O campo Percentual de Multa é obrigatório")]
        public int FinePercentage { get; private set; }
    }
}
