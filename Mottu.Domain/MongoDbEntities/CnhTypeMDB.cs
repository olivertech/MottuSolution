using Mottu.Domain.MongoDbEntities.Base;

namespace Mottu.Domain.MongoDbEntities
{
    /// <summary>
    /// Classe de domínio
    /// </summary>
    public sealed class CnhTypeMDB : BaseEntityMDB
    {
        #region Propriedades

        public string? Name { get; set; }

        #endregion
    }
}
