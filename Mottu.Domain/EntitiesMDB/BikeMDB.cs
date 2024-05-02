using Mottu.Domain.EntitiesMDB.Base;

namespace Mottu.Domain.EntitiesMDB
{
    public sealed class BikeMDB : BaseEntityMDB
    {
        #region Propriedades

        private string? _model { get; set; }

        public string? Model 
        {
            get
            { 
                return _model;
            }
            set
            { 
                if(value is not null)
                    _model = value.ToUpper();
            }
        }

        private string? _plate{ get; set; }

        public string? Plate
        {
            get
            {
                return _plate;
            }
            set
            {
                if (value is not null)
                    _plate = value.ToUpper();
            }
        }

        public string? Year { get; private set; }
        public bool IsLeased { get; set; }

        #endregion
    }
}
