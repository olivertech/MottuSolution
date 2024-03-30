using AutoMapper;
using Mottu.Application.Interfaces;
using Mottu.Application.Requests;
using Mottu.Application.Services.Base;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;

namespace Mottu.Application.Services
{
    public class UploadFileService : ServiceBase<StatusOrder, StatusOrderRequest>, IStatusOrderService
    {
        protected readonly IMapper? _mapper;

        public UploadFileService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
