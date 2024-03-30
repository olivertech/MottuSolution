using Mottu.Application.Helpers;

namespace Mottu.Application.Services
{
    public static class ServiceResponse<T>
    {
        public static EnumStatusCode StatusCode { get; set; }
        public static string? Message { get; set; }
        public static T? Response { get; set; }
    }
}
