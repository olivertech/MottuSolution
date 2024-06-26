﻿namespace Mottu.Domain.Entities
{
    /// <summary>
    /// Classe de domínio
    /// </summary>
    public sealed class UserType : BaseEntity
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

        #region Construtores

        public UserType()
        {
        }

        public UserType(string name)
        {
            Validate(name);
        }

        [JsonConstructor]
        public UserType(Guid id, string name)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(name);
        }

        #endregion

        #region Validações

        /// TODO: REAVALIAR SE VAI TER COMO USAR ESSAS VALIDAÇÕES
        
        private void Validate(string name)
        {
            DomainValidation.When(name == null, "campo 'nome' é obrigatório");

            Name = name!.ToUpper();
        }

        #endregion
    }
}
