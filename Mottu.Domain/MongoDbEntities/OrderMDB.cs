using Mottu.Domain.MongoDbEntities.Base;

namespace Mottu.Domain.MongoDbEntities
{
    public sealed class OrderMDB : BaseEntityMDB
    {
        #region Propriedades

        public string? Description { get; set; }
        public DateOnly DateOrder { get; set; }
        public double ValueOrder { get; set; }

        //Navigation Property
        public StatusOrder? StatusOrder { get; set; }

        // Many-to-many relation
        public IList<AcceptedOrder>? AcceptedOrders { get; set; }
        public IList<DeliveredOrder>? DeliveredOrders { get; set; }

        #endregion
    }
}
