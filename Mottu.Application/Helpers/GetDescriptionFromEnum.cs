using System.Runtime.Serialization;

namespace Mottu.Application.Helpers
{
    public static class GetDescriptionFromEnum
    {
        public static string GetFromUserTypeEnum(EnumUserTypes value)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            EnumMemberAttribute? attribute = value!.GetType()
                                                .GetField(value!.ToString())
                                                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                                                .SingleOrDefault() as EnumMemberAttribute;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return attribute == null ? value.ToString() : attribute.Value!;
        }

        public static string GetFromStatusOrderEnum(EnumStatusOrders value)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            EnumMemberAttribute? attribute = value!.GetType()
                                                .GetField(value!.ToString())
                                                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                                                .SingleOrDefault() as EnumMemberAttribute;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return attribute == null ? value.ToString() : attribute.Value!;
        }

        public static string GetFromStatusCnhType(EnumCnhTypes value)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            EnumMemberAttribute? attribute = value!.GetType()
                                                .GetField(value!.ToString())
                                                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                                                .SingleOrDefault() as EnumMemberAttribute;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return attribute == null ? value.ToString() : attribute.Value!;
        }
    }
}
