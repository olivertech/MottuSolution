using Mottu.Application.InterfacesMDB;
using Mottu.Domain.EntitiesMDB;

namespace Mottu.Application.ResponsesMDB
{
    public class AppUserResponseMDB : IResponseMDB
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; private set; }

        [JsonProperty(PropertyName = "login")]
        public string? Login { get; private set; }

        [JsonProperty(PropertyName = "password")]
        public string? Password { get; set; }

        [JsonProperty(PropertyName = "cnpj")]
        public string? Cnpj { get; private set; }

        [JsonProperty(PropertyName = "birthdate")]
        public DateOnly BirthDate { get; private set; }

        [JsonProperty(PropertyName = "cnh")]
        public string? Cnh { get; private set; }

        [JsonProperty(PropertyName = "path_cnh_image")]
        public string? PathCnhImage { get; private set; }

        [JsonProperty(PropertyName = "address")]
        public string? Address { get; private set; }

        [JsonProperty(PropertyName = "state")]
        public string? State { get; private set; }

        [JsonProperty(PropertyName = "city")]
        public string? City { get; private set; }

        [JsonProperty(PropertyName = "neighborhood")]
        public string? Neighborhood { get; private set; }

        [JsonProperty(PropertyName = "zipcode")]
        public string? ZipCode { get; private set; }

        [JsonProperty(PropertyName = "is_active")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "user_type")]
        public UserTypeMDB? UserType { get; set; }

        [JsonProperty(PropertyName = "cnh_type")]
        public CnhTypeMDB? CnhType { get; private set; }
    }
}
