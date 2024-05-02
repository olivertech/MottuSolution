using Mottu.Domain.EntitiesMDB.Base;

namespace Mottu.Domain.EntitiesMDB
{
    public sealed class NotificationMDB : BaseEntityMDB
    {
        #region Propriedades

        public DateOnly NotificationDate { get; set; }

        //Navigation Properties
        public Order? Order { get; set; }

        // Many-to-many relation
        public IList<NotificatedUser>? NotificatedUsers { get; set; }

        #endregion
    }
}
