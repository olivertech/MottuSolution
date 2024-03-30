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
