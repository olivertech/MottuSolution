namespace Mottu.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        private IAcceptedOrderRepository? _acceptedOrderRepository;
        private IBikeRepository? _bikeRepository;
        private ICnhTypeRepository? _cnhTypeRepository;
        private INotificatedUserRepository? _notificatedUserRepository;
        private INotificationRepository? _notificationRepository;
        private IOrderRepository? _orderRepository;
        private IPlanRepository? _planRepository;
        private IRentalRepository? _rentalRepository;
        private IStatusOrderRepository? _statusOrderRepository;
        private IAppUserRepository? _userRepository;
        private IUserTypeRepository? _userTypeRepository;
        private IDeliveredOrderRepository? _deliveredOrderRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IAcceptedOrderRepository acceptedOrderRepository
        {
            get
            {
                return _acceptedOrderRepository = _acceptedOrderRepository ?? new AcceptedOrderRepository(_context);
            }
        }

        public IBikeRepository bikeRepository
        {
            get
            {
                return _bikeRepository = _bikeRepository ?? new BikeRepository(_context);
            }
        }

        public ICnhTypeRepository cnhTypeRepository
        {
            get
            {
                return _cnhTypeRepository = _cnhTypeRepository ?? new CnhTypeRepository(_context);
            }
        }

        public INotificatedUserRepository notificatedUserRepository
        {
            get
            {
                return _notificatedUserRepository = _notificatedUserRepository ?? new NotificatedUserRepository(_context);
            }
        }

        public INotificationRepository notificationRepository
        {
            get
            {
                return _notificationRepository = _notificationRepository ?? new NotificationRepository(_context);
            }
        }

        public IOrderRepository orderRepository
        {
            get
            {
                return _orderRepository = _orderRepository ?? new OrderRepository(_context);
            }
        }

        public IPlanRepository planRepository
        {
            get
            {
                return _planRepository = _planRepository ?? new PlanRepository(_context);
            }
        }

        public IRentalRepository rentalRepository
        {
            get
            {
                return _rentalRepository = _rentalRepository ?? new RentalRepository(_context);
            }
        }

        public IStatusOrderRepository statusOrderRepository
        {
            get
            {
                return _statusOrderRepository = _statusOrderRepository ?? new StatusOrderRepository(_context);
            }
        }

        public IAppUserRepository userRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new AppUserRepository(_context);
            }
        }

        public IUserTypeRepository userTypeRepository
        {
            get
            {
                return _userTypeRepository = _userTypeRepository ?? new UserTypeRepository(_context);
            }
        }

        public IDeliveredOrderRepository deliveredOrderRepository
        {
            get
            {
                return _deliveredOrderRepository = _deliveredOrderRepository ?? new DeliveredOrderRepository(_context);
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
