﻿using Mottu.Application.RequestsMDB.Base;

namespace Mottu.Application.RequestsMDB
{
    public class BikeRequestUpdateMDB : BaseRequestMDB, IRequest
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        [Required(ErrorMessage = "O campo Id é obrigatório")]
        public string? Id { get; set; }

        [JsonPropertyName("model")]
        [JsonProperty(PropertyName = "model")]
        public string? Model { get; private set; }

        private string? plate;

        [JsonPropertyName("plate")]
        [JsonProperty(PropertyName = "plate")]
        [Required(ErrorMessage = "O campo Placa é obrigatório")]
        [StringLength(7, ErrorMessage = "Informe uma placa que tenha 7 caracteres.")]
        public string? Plate
        {
            get
            {
                return plate!.ToUpper();
            }
            set
            {
                plate = value!.ToUpper();
            }
        }

        [JsonPropertyName("year")]
        [JsonProperty(PropertyName = "year")]
        [Required(ErrorMessage = "O campo Ano é obrigatório")]
        public string? Year { get; set; }
    }
}
