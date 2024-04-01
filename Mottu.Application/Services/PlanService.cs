
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

        public override async Task<ServiceResponseFactory<Plan>> GetById(Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entity = await _unitOfWork!.planRepository.GetById(id);

            if (entity == null)
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            return ServiceResponseFactory<Plan>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Plano recuperado com sucesso.", entity!);
        }

        public async Task<ServiceResponseFactory<IEnumerable<Plan>>> GetListByName(string name)
        {
            if (name is null)
                return ServiceResponseFactory<IEnumerable<Plan>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Nome inválido!");

            var entities = await _unitOfWork!.planRepository.GetList(x => x.Name!.ToLower() == name.ToLower());

            if (entities != null)
                return ServiceResponseFactory<IEnumerable<Plan>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Plano recuperado por nome com sucesso.", entities);
            else
                return ServiceResponseFactory<IEnumerable<Plan>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não existe Plano para o nome informado!");
        }

        public override async Task<ServiceResponseFactory<int>> GetCount()
        {
            var numEntities = await _unitOfWork!.planRepository.Count();
            return ServiceResponseFactory<int>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Total de Planos recuperado com sucesso.", numEntities);
        }

        public override async Task<ServiceResponseFactory<Plan>> Insert(PlanRequest request)
        {
            if (request is null)
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            var search = _unitOfWork!.planRepository.GetAll().Result;
            
            if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
            {
                var existName = search!.Where(x => x.Name == request.Name).FirstOrDefault();
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Plano com esse nome.", existName!);
            }

            if (search!.Any(x => x.DailyValue == request.DailyValue) || search!.Any(x => x.NumDays == request.NumDays))
            {
                var existValues = search!.Where(x => x.DailyValue == request.DailyValue || x.NumDays == request.NumDays).FirstOrDefault();
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Plano com esses valores de diária e dias de locação.", existValues!);
            }

            var entity = _mapper!.Map<Plan>(request);

            entity.Id = Guid.NewGuid();
            entity.Name = request.Name!;

            var result = await _unitOfWork.planRepository.Insert(entity);

            _unitOfWork.CommitAsync().Wait();

            if (result != null)
                return ServiceResponseFactory<Plan>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Inclusão de Plano realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não foi possível incluir o Plano.", entity);
        }

        public override async Task<ServiceResponseFactory<Plan>> Update(PlanRequest request)
        {
            if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            var entity = _unitOfWork!.planRepository.GetById(request.Id).Result;

            if (entity is null)
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado inválido.", entity!);

            var search = _unitOfWork!.planRepository.GetAll().Result;

            if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
            {
                var existName = search!.Where(x => x.Name == request.Name).FirstOrDefault();
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Plano com esse nome.", existName!);
            }

            if (search!.Any(x => x.DailyValue == request.DailyValue) || search!.Any(x => x.NumDays == request.NumDays))
            {
                var existValues = search!.Where(x => x.DailyValue == request.DailyValue || x.NumDays == request.NumDays).FirstOrDefault();
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Plano com esses valores de diária e dias de locação.", existValues!);
            }

            request.Name = request.Name!.ToUpper();

            _mapper!.Map(request, entity);

            var result = await _unitOfWork.planRepository.Update(entity);

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<Plan>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Atualização de Plano realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status304NotModified, "Não foi possível atualizar o Plano", entity!);
        }

        public override async Task<ServiceResponseFactory<Plan>> Delete(PlanRequest request)
        {
            if (!Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entity = await _unitOfWork!.planRepository.GetById(request.Id);

            if (entity is null)
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status404NotFound, "Id informado inválido!");

            var result = await _unitOfWork.planRepository.Delete(request.Id);

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<Plan>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Remoção do Plano realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<Plan>.Return(false, Application.Helpers.EnumStatusCode.Status304NotModified, "Não foi possível remover o Plano", entity!);
        }
    }
}
