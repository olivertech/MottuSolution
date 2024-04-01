namespace Mottu.Application.Services
{
    public class NotificationService : ServiceBase<Notification, NotificationRequest>, INotificationService
    {
        protected readonly IMapper? _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
