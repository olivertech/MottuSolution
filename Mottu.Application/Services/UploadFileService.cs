namespace Mottu.Application.Services
{
    public class UploadFileService : ServiceBase<StatusOrder, StatusOrderRequest> //, IStatusOrderService
    {
        protected readonly IMapper? _mapper;

        public UploadFileService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
