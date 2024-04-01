namespace Mottu.Api.Controllers.Base
{
    public class ControllerBase<E, R> : Controller
        where E : IEntity
        where R : IResponse
    {
        protected readonly IUnitOfWork? _unitOfWork;
        protected readonly IMapper? _mapper;
        protected string? _nomeEntidade;

        protected IAcceptedOrderService? _acceptedOrderService;
        protected IAppUserService? _appUserService;
        protected IBikeService? _bikeService;
        protected ICnhTypeService? _cnhTypeService;
        protected IDeliveredOrderService? _deliveredOrderService;
        protected INotificatedUserService? _notificatedUserService;
        protected IOrderService? _orderService;
        protected IPlanService? _planService;
        protected IRentalService? _rentalService;
        protected IStatusOrderService? _statusOrderService;
        protected IUserTypeService? _userTypeService;

        public ControllerBase(IUnitOfWork unitOfWork, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
