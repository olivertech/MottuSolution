using AutoMapper;
using Mottu.CrossCutting.Responses;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Services
{
    public class CnHTypeService : IService<CnhType, CnhTypeResponse>
    {
        protected readonly IUnitOfWork? _unitOfWork;
        protected readonly IMapper? _mapper;

        public CnHTypeService(IUnitOfWork unitOfWork, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CnhType>> GetAll()
        {
            var list = await _unitOfWork!.cnhTypeRepository.GetAll();
            return list!;
        }

        public Task<int> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CnhType>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<CnhType> Insert(CnhTypeResponse request)
        {
            throw new NotImplementedException();
        }

        public Task<CnhType> Update(CnhTypeResponse request)
        {
            throw new NotImplementedException();
        }

        public Task<CnhType> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
