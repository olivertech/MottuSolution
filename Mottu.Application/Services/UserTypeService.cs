namespace Mottu.Application.Services
{
    public class UserTypeService : ServiceBase<UserType, UserTypeRequest>, IUserTypeService
    {
        protected readonly IMapper? _mapper;

        public UserTypeService(IUnitOfWork unitOfWork, IMapper? mapper) 
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<ServiceResponseFactory<IEnumerable<UserType>>> GetAll()
        {
            var list = await _unitOfWork!.userTypeRepository!.GetAll();
            return ServiceResponseFactory<IEnumerable<UserType>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Tipos de Usuário recuperada com sucesso.", list!);
        }

        public override async Task<ServiceResponseFactory<UserType>> GetById(Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entity = await _unitOfWork!.userTypeRepository.GetById(id);

            if (entity == null)
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            return ServiceResponseFactory<UserType>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Tipo de usuário recuperado com sucesso.", entity!);
        }

        public async Task<ServiceResponseFactory<IEnumerable<UserType>>> GetListByName(string name)
        {
            if (name is null)
                return ServiceResponseFactory<IEnumerable<UserType>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Nome inválido!");

            var entities = await _unitOfWork!.userTypeRepository.GetList(x => x.Name!.ToLower() == name.ToLower());

            if (entities != null)
                return ServiceResponseFactory<IEnumerable<UserType>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Tipo de usuário recuperado por nome com sucesso.", entities);
            else
                return ServiceResponseFactory<IEnumerable<UserType>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não existe tipos de usuário para o nome informado!");
        }

        public override async Task<ServiceResponseFactory<int>> GetCount()
        {
            var numEntities = await _unitOfWork!.userTypeRepository.Count();
            return ServiceResponseFactory<int>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Total de Tipos de Usuário recuperado com sucesso.", numEntities);
        }

        public override async Task<ServiceResponseFactory<UserType>> Insert(UserTypeRequest request)
        {
            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<UserType>.Return(false, validation.StatusCode, validation.Message!);

            if (request is null)
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            var search = _unitOfWork!.userTypeRepository.GetAll().Result;

            if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
            {
                var existEntity = search!.Where(x => x.Name == request.Name).FirstOrDefault();
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Tipo de Usuário com esse nome.", existEntity!);
            }

            var entity = _mapper!.Map<UserType>(request);

            entity.Id = Guid.NewGuid();
            entity.Name = request.Name!.ToUpper();

            var result = await _unitOfWork.userTypeRepository.Insert(entity);

            _unitOfWork.CommitAsync().Wait();

            if (result != null)
                return ServiceResponseFactory<UserType>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Inclusão de Tipo de Usuário realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não foi possível incluir o Tipo de Usuário.", entity);
        }

        public override async Task<ServiceResponseFactory<UserType>> Update(UserTypeRequest request)
        {
            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<UserType>.Return(false, validation.StatusCode, validation.Message!);

            if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            var entity = _unitOfWork!.userTypeRepository.GetById(request.Id).Result;

            if (entity is null)
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado inválido.", entity!);

            var search = await _unitOfWork!.userTypeRepository.GetAll();

            if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
            {
                var existEntity = search!.Where(x => x.Name == request.Name).FirstOrDefault();
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Tipo de Usuário com esse nome.", existEntity!);
            }

            request.Name = request.Name!.ToUpper();

            _mapper!.Map(request, entity);

            var result = await _unitOfWork.userTypeRepository.Update(entity);

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<UserType>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Atualização de Tipo de Usuário realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status304NotModified, "Não foi possível atualizar o Tipo de Usuário", entity!);
        }

        public override async Task<ServiceResponseFactory<UserType>> Delete(UserTypeRequest request)
        {
            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<UserType>.Return(false, validation.StatusCode, validation.Message!);

            if (!Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado igual a 0!");

            var entity = await _unitOfWork!.userTypeRepository.GetById(request.Id);

            if (entity is null)
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status404NotFound, "Id informado inválido!");

            var result = await _unitOfWork.userTypeRepository.Delete(request.Id);

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<UserType>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Remoção do Tipo de Usuário realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<UserType>.Return(false, Application.Helpers.EnumStatusCode.Status304NotModified, "Não foi possível remover o Tipo de Usuário", entity!);
        }
    }
}
