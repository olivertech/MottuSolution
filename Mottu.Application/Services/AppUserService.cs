namespace Mottu.Application.Services
{
    public class AppUserService : ServiceBase<AppUser, AppUserRequest>, IAppUserService
    {
        protected readonly IMapper? _mapper;

        public AppUserService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<ServiceResponseFactory<IEnumerable<AppUser>>> GetAll()
        {
            var list = await _unitOfWork!.userRepository!.GetAll();
            return ServiceResponseFactory<IEnumerable<AppUser>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Usuários recuperada com sucesso.", list!);
        }
    }
}
