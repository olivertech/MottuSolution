using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Application.Services;
using Mottu.Domain.Entities;

namespace Mottu.Application.Interfaces
{
    public interface INotificatedUserService : IServiceBase<NotificatedUser, NotificatedUserRequest>
    {
        Task<ServiceResponseFactory<ListNotificatedUsersResponse>> GetListOfNotificatedUsers(NotificatedUserRequest request);
    }
}
