
using Microsoft.AspNetCore.Http;

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

        public override Task<ServiceResponseFactory<CnhType>> GetById(Guid id)
        {
            return base.GetById(id);
        }

        public async Task<ServiceResponseFactory<IEnumerable<CnhType>>> GetListByName(string name)
        {
            if (name is null)
                return ServiceResponseFactory<IEnumerable<CnhType>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Nome inválido!");

            var entities = await _unitOfWork!.cnhTypeRepository.GetList(x => x.Name!.ToLower() == name.ToLower());

            if (entities != null)
                return ServiceResponseFactory<IEnumerable<CnhType>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Tipo de CNH recuperado por nome com sucesso.", entities);
            else
                return ServiceResponseFactory<IEnumerable<CnhType>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não existe Tipo de CNH para o nome informado!");
        }

        public override async Task<ServiceResponseFactory<int>> GetCount()
        {
            var numEntities = await _unitOfWork!.cnhTypeRepository.Count();
            return ServiceResponseFactory<int>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Total de Tipo de CNH recuperado com sucesso.", numEntities);
        }

        public override async Task<ServiceResponseFactory<CnhType>> Insert(CnhTypeRequest request)
        {
            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<CnhType>.Return(false, validation.StatusCode, validation.Message!);

            if (request is null)
                return ServiceResponseFactory<CnhType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            var search = _unitOfWork!.cnhTypeRepository.GetAll().Result;

            if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
            {
                var existEntity = search!.Where(x => x.Name == request.Name).FirstOrDefault();
                return ServiceResponseFactory<CnhType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Tipo de CNH com esse nome.", existEntity!);
            }

            var entity = _mapper!.Map<CnhType>(request);

            entity.Id = Guid.NewGuid();
            var result = _unitOfWork.cnhTypeRepository.Insert(entity);

            _unitOfWork.CommitAsync().Wait();

            if (result != null)
                return ServiceResponseFactory<CnhType>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Inclusão de Tipo de CNH realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<CnhType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não foi possível incluir o Tipo de CNH.", entity);
        }

        public override async Task<ServiceResponseFactory<CnhType>> Update(CnhTypeRequest request)
        {
            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<CnhType>.Return(false, validation.StatusCode, validation.Message!);

            if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<CnhType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado inválido!");

            var entity = _unitOfWork!.cnhTypeRepository.GetById(request.Id).Result;

            if (entity is null)
                return ServiceResponseFactory<CnhType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado inválido.", entity!);

            var search = _unitOfWork!.cnhTypeRepository.GetAll().Result;

            if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
            {
                var existEntity = search!.Where(x => x.Name == request.Name).FirstOrDefault();
                return ServiceResponseFactory<CnhType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Tipo de CNH com esse nome.", existEntity!);
            }

            _mapper!.Map(request, entity);

            var result = _unitOfWork.cnhTypeRepository.Update(entity).Result;

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<CnhType>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Atualização de Tipo de CNH realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<CnhType>.Return(false, Application.Helpers.EnumStatusCode.Status304NotModified, "Não foi possível atualizar o Tipo de CNH", entity!);
        }

        public override async Task<ServiceResponseFactory<CnhType>> Delete(CnhTypeRequest request)
        {
            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<CnhType>.Return(false, validation.StatusCode, validation.Message!);

            if (!Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<CnhType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado igual a 0!");

            var entity = _unitOfWork!.cnhTypeRepository.GetById(request.Id).Result;

            if (entity is null)
                return ServiceResponseFactory<CnhType>.Return(false, Application.Helpers.EnumStatusCode.Status404NotFound, "Id informado inválido!");

            var result = await _unitOfWork.cnhTypeRepository.Delete(request.Id);

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<CnhType>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Remoção do Tipo de CNH realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<CnhType>.Return(false, Application.Helpers.EnumStatusCode.Status304NotModified, "Não foi possível remover o Tipo de CNH.", entity!);
        }
    }
}
