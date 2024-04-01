namespace Mottu.Application.Interfaces
{
    public interface INotificatedUserService : IServiceBase<NotificatedUser, NotificatedUserRequest>
    {
        Task<ServiceResponseFactory<ListNotificatedUsersResponse>> GetListOfNotificatedUsers(NotificatedUserRequest request);
    }
}
