using Mottu.CrossCutting.Interfaces;

namespace Mottu.CrossCutting.Responses
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
