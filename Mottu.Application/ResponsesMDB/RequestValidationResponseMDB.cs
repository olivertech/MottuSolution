namespace Mottu.Application.ResponsesMDB
{
    public class RequestValidationResponseMDB
    {
        public RequestValidationResponseMDB(bool isValid, string userId, EnumStatusCode statusCode, string message)
        {
            UserId = userId;
            StatusCode = statusCode;
            Message = message;
            IsValid = isValid;
        }

        public string? UserId { get; set; }
        public EnumStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public bool IsValid { get; set; }
    }
}
