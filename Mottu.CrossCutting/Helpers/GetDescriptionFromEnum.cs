using System.Runtime.Serialization;

namespace Mottu.CrossCutting.Helpers
{
    public static class GetDescriptionFromEnum
    {
        public static string? GetFromUserTypeEnum(EnumUserTypes value)
        {
            EnumMemberAttribute? attribute = value!.GetType()
                                                .GetField(value!.ToString())
                                                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                                                .SingleOrDefault() as EnumMemberAttribute;

            return attribute == null ? value.ToString() : attribute.Value;
        }

        public static string? GetFromStatusOrderEnum(EnumStatusOrders value)
        {
            EnumMemberAttribute? attribute = value!.GetType()
                                                .GetField(value!.ToString())
                                                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                                                .SingleOrDefault() as EnumMemberAttribute;

            return attribute == null ? value.ToString() : attribute.Value;
        }

        public static string? GetFromStatusCnhType(EnumCnhTypes value)
        {
            EnumMemberAttribute? attribute = value!.GetType()
                                                .GetField(value!.ToString())
                                                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                                                .SingleOrDefault() as EnumMemberAttribute;

            return attribute == null ? value.ToString() : attribute.Value;
        }
    }
}
