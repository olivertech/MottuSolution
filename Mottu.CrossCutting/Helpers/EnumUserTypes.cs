﻿using System.Runtime.Serialization;

namespace Mottu.CrossCutting.Helpers
{
    public enum EnumUserTypes
    {
        [EnumMember(Value = "Administrador")]
        Administrador = 1,
        [EnumMember(Value = "Entregador")]
        Entregador = 2,
        [EnumMember(Value = "Consumidor")]
        Consumidor = 3,
    }
}