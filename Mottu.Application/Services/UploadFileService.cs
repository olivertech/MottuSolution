using AutoMapper;
using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
using Mottu.Application.Responses;
using Mottu.Application.Services.Base;
using Mottu.Domain.Entities;
using Mottu.Domain.Entities.Base;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Services
{
    public class UploadFileService : ServiceBase<StatusOrder, StatusOrderResponse>, IStatusOrderService
    {
        protected readonly IMapper? _mapper;

        public UploadFileService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
