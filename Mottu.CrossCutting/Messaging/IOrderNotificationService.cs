namespace Mottu.Application.Messaging
{
    public interface IOrderNotificationService
    {
        //Task<bool> OnOrderNotificationAsync(INotificationMessage notification);
        void OnOrderNotificationAsync(INotificationMessage notification);
    }
}
