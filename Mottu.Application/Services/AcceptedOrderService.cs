using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
using Mottu.Application.Responses;
using Mottu.Application.Services;
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
    public class AcceptedOrderService : ServiceBase<AcceptedOrder, AcceptedOrderResponse>, IAcceptedOrderService
    {
        protected readonly IMapper? _mapper;

        public AcceptedOrderService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public ServiceResponseFactory<AppUserResponse> AcceptOrder(Guid userId, Guid orderId)
        {
            //Valida usuario
            if (!Guid.TryParse(userId.ToString(), out _))
                return ServiceResponseFactory<AppUserResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id do usuário inválido!");

            var user = _unitOfWork!.userRepository.GetById(userId).Result;

            if (user == null)
                return ServiceResponseFactory<AppUserResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Usuário inválido.");


            return ServiceResponseFactory<AppUserResponse>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "OK");
        }
    }
}
