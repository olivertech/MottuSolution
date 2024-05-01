using Mottu.Domain.MongoDbEntities.Base;

namespace Mottu.Domain.MongoDbEntities
{
    public sealed class PlanMDB : BaseEntityMDB
    {
        #region Propriedades

        public string? Name { get; set; }
        public string? Description { get; set; }
        public int NumDays { get; private set; }
        public double DailyValue { get; private set; }
        public int FinePercentage { get; private set; }

        #endregion
    }
}
