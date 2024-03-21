using AutoMapper;
using Mottu.Application.Helpers;
using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
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
    public class CnhTypeService : ServiceBase<CnhType, CnhTypeResponse>, ICnhTypeService
    {
        protected readonly IMapper? _mapper;

        public CnhTypeService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async override Task<IEnumerable<CnhType>> GetAll()
        {
            var convert = new ConvertModelToResponse<CnhType, CnhTypeResponse>(_mapper);

            var list = await _unitOfWork!.cnhTypeRepository.GetAll();
            List<CnhTypeResponse> result = convert.GetResponsList(list!);

            return list!;
        }
    }
}
