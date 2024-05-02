using Mottu.Application.InterfacesMDB.Base;

namespace Mottu.Application.InterfacesMDB
{
    public interface INotificatedUserServiceMDB : IServiceBaseMDB<NotificatedUser, NotificatedUserRequest>
    {
        Task<ServiceResponseFactory<ListNotificatedUsersResponse>> GetListOfNotificatedUsers(NotificatedUserRequest request);
    }
}
