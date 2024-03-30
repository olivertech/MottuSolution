using Mottu.Application.Responses;
using Mottu.Application.Services;
using Mottu.Domain.Entities;
using Mottu.Domain.Entities.Base;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Interfaces.Base
{
    public interface IServiceBase<E, R>
        where E : IEntity
        where R : IRequest
    {
        Task<ServiceResponseFactory<IEnumerable<E>>> GetAll();
        Task<ServiceResponseFactory<E>> GetById(Guid id);
        Task<ServiceResponseFactory<int>> GetCount();
        Task<ServiceResponseFactory<E>> Insert(R request);
        Task<ServiceResponseFactory<E>> Update(R request);
        Task<ServiceResponseFactory<E>> Delete(Guid id);
    }
}
