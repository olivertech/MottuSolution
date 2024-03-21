using AutoMapper;
using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
using Mottu.Application.Responses;
using Mottu.Application.Services.Base;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Services
{
    public class NotificatedUserService : ServiceBase<NotificatedUser, NotificatedUserResponse>, INotificatedUserService
    {
        protected readonly IMapper? _mapper;

        public NotificatedUserService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
