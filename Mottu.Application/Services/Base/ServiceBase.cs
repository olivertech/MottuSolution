using Mottu.Application.Helpers;
using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
using Mottu.Application.Responses;
using Mottu.Domain.Entities;
using Mottu.Domain.Entities.Base;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Services.Base
{
    public class ServiceBase<E, R> : IServiceBase<E, R>
        where E : IEntity
        where R : IResponse
    {
        protected readonly IUnitOfWork? _unitOfWork;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Método que valida se 
        /// o usuário da requisição
        /// é um administrador
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected async Task<RequestValidationResponse> ValidateRequester(Guid userId)
        {
            //Valido solicitante da requisição
            var requester = await _unitOfWork!.userRepository.GetFullById(userId);

            if (requester is null)
                return new RequestValidationResponse(false, userId, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            if (requester.UserType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromUserTypeEnum(EnumUserTypes.Administrador).ToLower())
                return new RequestValidationResponse(false, userId, Application.Helpers.EnumStatusCode.Status400BadRequest, "Usuário solicitante inválido!");

            return new RequestValidationResponse(true, userId, EnumStatusCode.Status200OK, "Usuário requisitante válido");
        }

        public virtual Task<IEnumerable<E>> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<E>> GetList()
        {
            throw new NotImplementedException();
        }

        public virtual Task<E> Insert(R request)
        {
            throw new NotImplementedException();
        }

        public virtual Task<E> Update(R request)
        {
            throw new NotImplementedException();
        }

        public virtual Task<E> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
