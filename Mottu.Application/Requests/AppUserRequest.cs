using Mottu.Application.Requests.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mottu.Application.Requests
{
    public class AppUserRequest : BaseRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        [JsonProperty(PropertyName = "name")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string? Name { get; set; }

        [JsonPropertyName("login")]
        [JsonProperty(PropertyName = "login")]
        [Required(ErrorMessage = "O campo Login é obrigatório")]
        public string? Login { get; set; }

        [JsonPropertyName("password")]
        [JsonProperty(PropertyName = "password")]
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string? Password { get; set; }

        [JsonPropertyName("cnpj")]
        [JsonProperty(PropertyName = "cnpj")]
        [Required(ErrorMessage = "O campo CNPJ é obrigatório")]
        public string? Cnpj { get; set; }

        [JsonPropertyName("cnh")]
        [JsonProperty(PropertyName = "cnh")]
        [Required(ErrorMessage = "O campo CNH é obrigatório")]
        public string? Cnh { get; set; }

        [JsonPropertyName("birthdate")]
        [JsonProperty(PropertyName = "birthdate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateOnly BirthDate { get; set; }

        [JsonPropertyName("path_cnh_image")]
        [JsonProperty(PropertyName = "path_cnh_image")]
        public string? PathCnhImage { get; set; }

        [JsonPropertyName("address")]
        [JsonProperty(PropertyName = "address")]
        public string? Address { get; set; }

        [JsonPropertyName("state")]
        [JsonProperty(PropertyName = "state")]
        public string? State { get; set; }

        [JsonPropertyName("city")]
        [JsonProperty(PropertyName = "city")]
        public string? City { get; set; }

        [JsonPropertyName("neighborhood")]
        [JsonProperty(PropertyName = "neighborhood")]
        public string? Neighborhood { get; set; }

        [JsonPropertyName("zipcode")]
        [JsonProperty(PropertyName = "zipcode")]
        public string? ZipCode { get; set; }

        [JsonPropertyName("is_active")]
        [JsonProperty(PropertyName = "is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("user_type_id")]
        [JsonProperty(PropertyName = "user_type_id")]
        [Required(ErrorMessage = "O campo Id Tipo Usuário é obrigatório")]
        public Guid? UserTypeId { get; set; }

        [JsonPropertyName("cnh_type_id")]
        [JsonProperty(PropertyName = "cnh_type_id")]
        [Required(ErrorMessage = "O campo Id Tipo CNH é obrigatório")]
        public Guid? CnhTypeId { get; set; }
    }
}
