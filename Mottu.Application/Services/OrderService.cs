﻿using AutoMapper;
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
    public class OrderService : ServiceBase<Order, OrderRequest>, IOrderService
    {
        protected readonly IMapper? _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<ServiceResponseFactory<IEnumerable<Order>>> GetAll()
        {
            var list = await _unitOfWork!.orderRepository!.GetAll();
            return ServiceResponseFactory<IEnumerable<Order>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Pedidos recuperada com sucesso.", list!);
        }
    }
}
