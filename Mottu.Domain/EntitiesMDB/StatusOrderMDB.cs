using Mottu.Domain.EntitiesMDB.Base;

namespace Mottu.Domain.EntitiesMDB
{
    /// <summary>
    /// Classe de domínio
    /// </summary>
    public sealed class StatusOrderMDB : BaseEntityMDB
    {
        #region Proriedades

        private string? _name { get; set; }

        public string? Name 
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null)
                    _name = value!.ToUpper();
            }
        }

        #endregion
    }
}
