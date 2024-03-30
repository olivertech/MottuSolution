using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Application.Services;
using Mottu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Interfaces
{
    public interface INotificatedUserService : IServiceBase<NotificatedUser, NotificatedUserRequest>
    {
        Task<ServiceResponseFactory<ListNotificatedUsersResponse>> GetListOfNotificatedUsers(NotificatedUserRequest request);
    }
}
