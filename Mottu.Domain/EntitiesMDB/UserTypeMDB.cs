﻿using Mottu.Domain.EntitiesMDB.Base;

namespace Mottu.Domain.EntitiesMDB
{
    /// <summary>
    /// Classe de domínio
    /// </summary>
    public sealed class UserTypeMDB : BaseEntityMDB
    {
        #region Propriedades

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
