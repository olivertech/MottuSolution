using Mottu.Application.Interfaces;

namespace Mottu.Application.Responses
{
    public class UploadResponse : IResponse
    {
        public UploadResponse()
        {
        }

        public UploadResponse(string mensagem)
        {
            this.Message = mensagem;
        }

        public string? Message { get; set; }
    }
}
