namespace Mottu.Api.Controllers
{
    /// <summary>
    /// Não tive tempo de implementar com o Minio,
    /// Então acabou ficando apenas com pasta local
    /// </summary>
    [Route("api/Upload")]
    [SwaggerTag("Upload")]
    [ApiController]
    public class UploadController : ControllerBase<IEntity, IResponse>
    {
        private readonly IMinioClient minioClient;

        public UploadController(IUnitOfWork unitOfWork, IMapper? mapper, IMinioClient minioClient)
            : base(unitOfWork, mapper)
        {
            this.minioClient = minioClient;
        }
    }
}
