namespace Mottu.Application.Services.Base
{
    public class ServiceBase<E, R> : IServiceBase<E, R>
        where E : IEntity
        where R : IRequest
    {
        protected readonly IUnitOfWork? _unitOfWork;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Método que valida se 
        /// o usuário da requisição
        /// é um administrador
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected async Task<RequestValidationResponse> ValidateAdminRequester(Guid userId)
        {
            //Valido solicitante da requisição
            var requester = await _unitOfWork!.userRepository.GetFullById(userId);

            if (requester is null)
                return new RequestValidationResponse(false, userId, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            var adminUserType = _unitOfWork.userTypeRepository.GetList(x => x.Name!.ToLower().Equals("administrador")).Result!.FirstOrDefault();

            if (requester.UserType!.Id != adminUserType!.Id)
                return new RequestValidationResponse(false, userId, Application.Helpers.EnumStatusCode.Status400BadRequest, "Usuário solicitante inválido!");

            return new RequestValidationResponse(true, userId, EnumStatusCode.Status200OK, "Usuário requisitante válido");
        }

        public virtual Task<ServiceResponseFactory<IEnumerable<E>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<ServiceResponseFactory<E>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ServiceResponseFactory<int>> GetCount()
        {
            throw new NotImplementedException();
        }

        public virtual Task<ServiceResponseFactory<E>> Insert(R request)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ServiceResponseFactory<E>> Update(R request)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ServiceResponseFactory<E>> Delete(R request)
        {
            throw new NotImplementedException();
        }
    }
}
