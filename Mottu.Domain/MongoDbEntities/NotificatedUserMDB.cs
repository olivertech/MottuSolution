using Mottu.Domain.MongoDbEntities.Base;

namespace Mottu.Domain.MongoDbEntities
{
    public sealed class NotificatedUserMDB : BaseEntityMDB
    {
        #region Propriedades

        public DateOnly NotificationDate { get; set; }

        //Navigation Properties
        public Guid? UserId { get; set; }
        public AppUser? User { get; private set; }

        public Guid? NotificationId { get; set; }
        public Notification? Notification { get; private set; }

        #endregion
    }
}
