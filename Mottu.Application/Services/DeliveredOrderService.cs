using AutoMapper;
using Mottu.Application.Interfaces;
using Mottu.Application.Requests;
using Mottu.Application.Services.Base;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;

namespace Mottu.Application.Services
{
    public class DeliveredOrderService : ServiceBase<DeliveredOrder, DeliveredOrderRequest>, IDeliveredOrderService
    {
        protected readonly IMapper? _mapper;

        public DeliveredOrderService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
