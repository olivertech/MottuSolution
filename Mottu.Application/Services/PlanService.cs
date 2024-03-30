using AutoMapper;
using Mottu.Application.Interfaces;
using Mottu.Application.Requests;
using Mottu.Application.Services.Base;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;

namespace Mottu.Application.Services
{
    public class PlanService : ServiceBase<Plan, PlanRequest>, IPlanService
    {
        protected readonly IMapper? _mapper;

        public PlanService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<ServiceResponseFactory<IEnumerable<Plan>>> GetAll()
        {
            var list = await _unitOfWork!.planRepository!.GetAll();
            return ServiceResponseFactory<IEnumerable<Plan>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Planos recuperada com sucesso.", list!);
        }
    }
}
