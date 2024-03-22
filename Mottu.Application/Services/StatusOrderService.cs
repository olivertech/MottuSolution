using AutoMapper;
using Mottu.Application.Helpers;
using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
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
    public class StatusOrderService : ServiceBase<StatusOrder, StatusOrderResponse>, IStatusOrderService
    {
        protected readonly IMapper? _mapper;

        public StatusOrderService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override Task<IEnumerable<StatusOrder>?> GetAll()
        {
            return _unitOfWork!.statusOrderRepository!.GetAll();
        }
    }
}
