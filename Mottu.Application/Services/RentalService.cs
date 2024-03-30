using AutoMapper;
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
    public class RentalService : ServiceBase<Rental, RentalRequest>, IRentalService
    {
        protected readonly IMapper? _mapper;

        public RentalService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<ServiceResponseFactory<IEnumerable<Rental>>> GetAll()
        {
            var list = await _unitOfWork!.rentalRepository!.GetFullAll();
            return ServiceResponseFactory<IEnumerable<Rental>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Locações recuperada com sucesso.", list!);
        }
    }
}
