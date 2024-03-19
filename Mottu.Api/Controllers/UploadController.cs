using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;

namespace Mottu.Api.Controllers
{
    /// <summary>
    /// Não tive tempo de implementar com o Minio,
    /// Então acabou ficando apenas com pasta local
    /// </summary>
    [Route("api/Upload")]
    [SwaggerTag("Upload")]
    [ApiController]
    public class UploadController : BaseController
    {
        private readonly IMinioClient minioClient;

        public UploadController(IUnitOfWork unitOfWork, IMapper? mapper, IMinioClient minioClient)
            : base(unitOfWork, mapper)
        {
            this.minioClient = minioClient;
        }
    }
}
