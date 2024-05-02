using Mottu.Application.InterfacesMDB;

namespace Mottu.Application.ResponsesMDB
{
    public class UploadResponseMDB : IResponseMDB
    {
        public UploadResponseMDB()
        {
        }

        public UploadResponseMDB(string mensagem)
        {
            this.Message = mensagem;
        }

        public string? Message { get; set; }
    }
}
