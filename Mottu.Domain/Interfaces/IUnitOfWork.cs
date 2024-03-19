namespace Mottu.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAcceptedOrderRepository acceptedOrderRepository { get; }
        IBikeRepository bikeRepository { get; }
        ICnhTypeRepository cnhTypeRepository { get; }
        INotificatedUserRepository notificatedUserRepository { get; }
        INotificationRepository notificationRepository { get; }
        IOrderRepository orderRepository { get; }
        IPlanRepository planRepository { get; }
        IRentalRepository rentalRepository { get; }
        IStatusOrderRepository statusOrderRepository { get; }
        IAppUserRepository userRepository { get; }
        IUserTypeRepository userTypeRepository { get; }
        IDeliveredOrderRepository deliveredOrderRepository { get; }

        Task CommitAsync();
    }
}
