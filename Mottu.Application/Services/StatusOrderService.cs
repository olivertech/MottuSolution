using AutoMapper;
using Mottu.Application.Helpers;
using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Application.Services.Base;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
