using System.Runtime.Serialization;

namespace Mottu.Application.Helpers
{
    public enum EnumStatusOrders
    {
        [EnumMember(Value = "Entregue")]
        Entregue = 1,
        [EnumMember(Value = "Aceito")]
        Aceito = 2,
        [EnumMember(Value = "Disponivel")]
        Disponivel = 3,
    }
}
