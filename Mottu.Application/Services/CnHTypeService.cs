using AutoMapper;
using Mottu.Application.Helpers;
using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
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
    public class CnhTypeService : ServiceBase<CnhType, CnhTypeRequest>, ICnhTypeService
    {
        protected readonly IMapper? _mapper;

        public CnhTypeService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<ServiceResponseFactory<IEnumerable<CnhType>>> GetAll()
        {
            var list = await _unitOfWork!.cnhTypeRepository!.GetAll();
            return ServiceResponseFactory<IEnumerable<CnhType>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Tipos de CNH recuperada com sucesso.", list!);
        }
    }
}
