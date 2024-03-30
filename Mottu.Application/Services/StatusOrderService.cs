using AutoMapper;
using Mottu.Application.Interfaces;
using Mottu.Application.Requests;
using Mottu.Application.Services.Base;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;

namespace Mottu.Application.Services
{
    public class StatusOrderService : ServiceBase<StatusOrder, StatusOrderRequest>, IStatusOrderService
    {
        protected readonly IMapper? _mapper;

        public StatusOrderService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<ServiceResponseFactory<IEnumerable<StatusOrder>>> GetAll()
        {
            var list = await _unitOfWork!.statusOrderRepository!.GetAll();
            return ServiceResponseFactory<IEnumerable<StatusOrder>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Status do Pedido recuperado com sucesso.", list!);
        }
    }
}
