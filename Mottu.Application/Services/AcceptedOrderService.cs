using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mottu.CrossCutting.Responses;
using Mottu.CrossCutting.Services;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Services
{
    public class AcceptedOrderService : IService<AcceptedOrder, AcceptedOrderResponse>
    {
        protected readonly IUnitOfWork? _unitOfWork;
        protected readonly IMapper? _mapper;

        public AcceptedOrderService(IUnitOfWork unitOfWork, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ServiceResponseFactory<AppUserResponse> AcceptOrder(Guid userId, Guid orderId)
        {
            //Valida usuario
            if (!Guid.TryParse(userId.ToString(), out _))
                return ServiceResponseFactory<AppUserResponse>.Return(false, CrossCutting.Helpers.EnumStatusCode.Status400BadRequest, "Id do usuário inválido!");

            var user = _unitOfWork!.userRepository.GetById(userId).Result;

            if (user == null)
                return ServiceResponseFactory<AppUserResponse>.Return(false, CrossCutting.Helpers.EnumStatusCode.Status400BadRequest, "Usuário inválido.");

            return ServiceResponseFactory<AppUserResponse>.Return(true, CrossCutting.Helpers.EnumStatusCode.Status200OK, "OK");
        }

        public Task<IEnumerable<AcceptedOrder>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AcceptedOrder>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<AcceptedOrder> Insert(AcceptedOrderResponse request)
        {
            throw new NotImplementedException();
        }

        public Task<AcceptedOrder> Update(AcceptedOrderResponse request)
        {
            throw new NotImplementedException();
        }

        public Task<AcceptedOrder> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
