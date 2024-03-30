namespace Mottu.Application.Services
{
    public class DeliveredOrderService : ServiceBase<DeliveredOrder, DeliveredOrderRequest>, IDeliveredOrderService
    {
        protected readonly IMapper? _mapper;

        public DeliveredOrderService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
