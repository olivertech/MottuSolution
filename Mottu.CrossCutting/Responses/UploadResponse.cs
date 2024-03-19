namespace Mottu.CrossCutting.Responses
{
    public class UploadResponse
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
