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
    public class UserTypeService : IService<UserType, UserTypeResponse>
    {
        protected readonly IUnitOfWork? _unitOfWork;
        protected readonly IMapper? _mapper;

        public UserTypeService(IUnitOfWork unitOfWork, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserType>> GetAll()
        {
            var list = await _unitOfWork!.userTypeRepository.GetAll();
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

        public Task<IEnumerable<UserType>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<UserType> Insert(UserTypeResponse request)
        {
            throw new NotImplementedException();
        }

        public Task<UserType> Update(UserTypeResponse request)
        {
            throw new NotImplementedException();
        }

        public Task<UserType> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
