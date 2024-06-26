﻿using Mottu.CrossCutting.Requests.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mottu.CrossCutting.Requests
{
    public class BikeByPlateRequest : BaseRequest
    {
        private string? plate;

        [JsonPropertyName("plate")]
        [JsonProperty(PropertyName = "plate")]
        [Required(ErrorMessage = "O campo Placa é obrigatório")]
        [StringLength(7, ErrorMessage = "Informe placa com 7 caracteres.")]
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
    }
}
