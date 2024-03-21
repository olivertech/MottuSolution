using AutoMapper;
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
    public class OrderService : ServiceBase<Order, OrderResponse>, IOrderService
    {
        protected readonly IMapper? _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
