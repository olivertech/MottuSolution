using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
using Mottu.Application.Responses;
using Mottu.Domain.Entities;
using Mottu.Domain.Entities.Base;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
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

        //public async Task<IEnumerable<UserType>> GetAll()
        //{
        //    var list = await _unitOfWork!.userTypeRepository.GetAll();
        //    return list!;
        //}

        //public Task<int> GetById(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> GetCount()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<UserType>> GetList()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<UserType> Insert(UserTypeResponse request)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<UserType> Update(UserTypeResponse request)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<UserType> Delete(Guid id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
