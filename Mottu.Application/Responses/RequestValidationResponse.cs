using Mottu.Application.Helpers;

namespace Mottu.Application.Responses
{
    public class RequestValidationResponse
    {
        public RequestValidationResponse(bool isValid, Guid userId, EnumStatusCode statusCode, string message)
        {
            UserId = userId;
            StatusCode = statusCode;
            Message = message;
            IsValid = isValid;
        }

        public Guid? UserId { get; set; }
        public EnumStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public bool IsValid { get; set; }
    }
}
