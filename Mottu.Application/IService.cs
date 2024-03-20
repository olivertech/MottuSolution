using Mottu.CrossCutting.Interfaces;
using Mottu.Domain.Entities.Base;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application
{
    public interface IService<E,R> 
        where E : IEntity
        where R : IResponse
    {
        Task<IEnumerable<E>> GetAll();
        Task<int> GetById(Guid id);
        Task<IEnumerable<E>> GetList();
        Task<int> GetCount();
        Task<E> Insert(R request);
        Task<E> Update(R request);
        Task<E> Delete(Guid id);
    }
}
