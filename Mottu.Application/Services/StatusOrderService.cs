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
    public class StatusOrderService : IService<StatusOrder, StatusOrderResponse>
    {
        protected readonly IUnitOfWork? _unitOfWork;
        protected readonly IMapper? _mapper;

        public StatusOrderService(IUnitOfWork unitOfWork, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<StatusOrder> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StatusOrder>> GetAll()
        {
            var list = await _unitOfWork!.statusOrderRepository.GetAll();
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

        public Task<IEnumerable<StatusOrder>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<StatusOrder> Insert(StatusOrderResponse request)
        {
            throw new NotImplementedException();
        }

        public Task<StatusOrder> Update(StatusOrderResponse request)
        {
            throw new NotImplementedException();
        }
    }
}
