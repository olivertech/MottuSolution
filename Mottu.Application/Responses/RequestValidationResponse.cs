using Mottu.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
