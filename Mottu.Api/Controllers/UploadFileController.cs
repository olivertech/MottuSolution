namespace Mottu.Api.Controllers
{
    /// <summary>
    /// Controller que recebe arquivos e salva localmente
    /// na pasta C:\\Mottu\\Images\\CNH\\
    /// </summary>
    [Route("api/UploadFile")]
    [SwaggerTag("UplodFile")]
    [ApiController]
    public class UploadFileController : ControllerBase<BaseEntity, UploadResponse>
    {
        public static IWebHostEnvironment? _environment;

        public UploadFileController(IUnitOfWork unitOfWork, IMapper? mapper, IWebHostEnvironment environment)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Upload";
            _environment = environment;
        }

        [HttpPost]
        [Route(nameof(Cnh))]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(UploadResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError, Type = typeof(UploadResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(UploadResponse))]
        public async Task<ActionResult<UploadResponse>> Cnh([FromForm] IFormFile arquivo, [FromForm] Guid userId)
        {
            if(userId == Guid.Empty || userId.ToString().Length == 0 || !Guid.TryParse(userId.ToString(), out _))
                return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory<UploadResponse>.Error("Id do usuário inválido!"));

            var user = _unitOfWork!.userRepository.GetById(userId).Result;

            if (user == null)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory<UploadResponse>.Error("Id do usuário inválido!"));

            if (arquivo == null)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory<UploadResponse>.Error("arquivo inválido!"));

            if((!arquivo.ContentType.ToLower().Contains("png")) && (!arquivo.ContentType.ToLower().Contains("bmp")))
                return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory<UploadResponse>.Error("Formato do arquivo inválido. Enviar apenas BMP ou PNG!"));


            if (arquivo.Length > 0)
            {
                try
                {
                    if (!Directory.Exists("C:\\Mottu\\Images\\CNH\\"))
                    {
                        Directory.CreateDirectory("C:\\Mottu\\Images\\CNH\\");
                    }

                    using (FileStream filestream = System.IO.File.Create("C:\\Mottu\\Images\\CNH\\" + arquivo.FileName))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();

                        //Atualizar usuário com o caminho da imagem enviada
                        user!.PathCnhImage = "C:\\Mottu\\Images\\CNH\\" + arquivo.FileName;
                        await _unitOfWork.userRepository.Update(user);

                        _unitOfWork.CommitAsync().Wait();

                        var uploadResponse = new UploadResponse("C:\\Mottu\\Images\\CNH\\" + arquivo.FileName);
                        return Ok(ResponseFactory<UploadResponse>.Success(String.Format("{0} Realizado Com Sucesso.", _nomeEntidade), uploadResponse));
                    }
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<UploadResponse>.Error("Não foi possível processar o arquivo!"));
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory<UploadResponse>.Error("arquivo inválido!"));
            }

        }
    }
}
