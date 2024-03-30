using AutoMapper;
using Mottu.Application.Interfaces;
using Mottu.Application.Requests;
using Mottu.Application.Services.Base;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;

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
