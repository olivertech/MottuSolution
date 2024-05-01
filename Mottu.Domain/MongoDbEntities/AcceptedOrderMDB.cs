﻿using Mottu.Domain.MongoDbEntities.Base;

namespace Mottu.Domain.MongoDbEntities
{
    public sealed class AcceptedOrderMDB : BaseEntityMDB
    {
        #region Propriedades

        public DateOnly? AcceptedDate { get; set; }

        //Navigation Properties
        public Guid UserId { get; set; }
        public AppUser? User { get; private set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; private set; }

        #endregion
    }
}
