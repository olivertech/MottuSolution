using Microsoft.AspNetCore.Http;

namespace Mottu.Application.Services
{
    public class StatusOrderService : ServiceBase<StatusOrder, StatusOrderRequest>, IStatusOrderService
    {
        protected readonly IMapper? _mapper;

        public StatusOrderService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<ServiceResponseFactory<IEnumerable<StatusOrder>>> GetAll()
        {
            var list = await _unitOfWork!.statusOrderRepository!.GetAll();
            return ServiceResponseFactory<IEnumerable<StatusOrder>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Status do Pedido recuperada com sucesso.", list!);
        }

        public override async Task<ServiceResponseFactory<StatusOrder>> GetById(Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entity = await _unitOfWork!.statusOrderRepository.GetById(id);

            if (entity == null)
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            return ServiceResponseFactory<StatusOrder>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Status do Pedido recuperado com sucesso.", entity!);
        }

        public async Task<ServiceResponseFactory<IEnumerable<StatusOrder>>> GetListByName(string name)
        {
            if (name is null)
                return ServiceResponseFactory<IEnumerable<StatusOrder>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Nome inválido!");

            var entities = await _unitOfWork!.statusOrderRepository.GetList(x => x.Name!.ToLower() == name.ToLower());

            if (entities != null)
                return ServiceResponseFactory<IEnumerable<StatusOrder>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Status do Pedido recuperado por nome com sucesso.", entities);
            else
                return ServiceResponseFactory<IEnumerable<StatusOrder>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não existe Status do Pedido para o nome informado!");
        }

        public override async Task<ServiceResponseFactory<int>> GetCount()
        {
            var numEntities = await _unitOfWork!.statusOrderRepository.Count();
            return ServiceResponseFactory<int>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Total de Status do Pedido recuperado com sucesso.", numEntities);
        }

        public override async Task<ServiceResponseFactory<StatusOrder>> Insert(StatusOrderRequest request)
        {
            if (request is null)
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<StatusOrder>.Return(false, validation.StatusCode, validation.Message!);

            var search = _unitOfWork!.statusOrderRepository.GetAll().Result;

            if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
            {
                var existEntity = search!.Where(x => x.Name == request.Name).FirstOrDefault();
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Status de Pedido com esse nome.", existEntity!);
            }

            var entity = _mapper!.Map<StatusOrder>(request);

            entity.Id = Guid.NewGuid();
            entity.Name = request.Name;

            var result = _unitOfWork.statusOrderRepository.Insert(entity);

            _unitOfWork.CommitAsync().Wait();

            if (result != null)
                return ServiceResponseFactory<StatusOrder>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Inclusão de Status de Pedido realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não foi possível incluir o Status de Pedido.", entity);
        }

        public override async Task<ServiceResponseFactory<StatusOrder>> Update(StatusOrderRequest request)
        {
            if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<StatusOrder>.Return(false, validation.StatusCode, validation.Message!);

            var entity = _unitOfWork!.statusOrderRepository.GetById(request.Id).Result;

            if (entity is null)
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado inválido.", entity!);

            var search = _unitOfWork!.statusOrderRepository.GetAll().Result;

            if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
            {
                var existEntity = search!.Where(x => x.Name == request.Name).FirstOrDefault();
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Status de Pedido com esse nome.", existEntity!);
            }

            _mapper!.Map(request, entity);

            var result = _unitOfWork.statusOrderRepository.Update(entity).Result;

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<StatusOrder>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Atualização de Status de Pedido realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status304NotModified, "Não foi possível atualizar o Status de Pedido", entity!);
        }

        public override async Task<ServiceResponseFactory<StatusOrder>> Delete(StatusOrderRequest request)
        {
            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<StatusOrder>.Return(false, validation.StatusCode, validation.Message!);

            if (!Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entity = await _unitOfWork!.statusOrderRepository.GetById(request.Id);

            if (entity is null)
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status404NotFound, "Id informado inválido!");

            var result = await _unitOfWork.userTypeRepository.Delete(request.Id);

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<StatusOrder>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Remoção do Status de Pedido realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<StatusOrder>.Return(false, Application.Helpers.EnumStatusCode.Status304NotModified, "Não foi possível remover o Status de Pedido.", entity!);
        }
    }
}
