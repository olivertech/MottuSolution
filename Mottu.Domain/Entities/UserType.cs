using Mottu.Domain.Entities.Base;
using Mottu.Domain.Validations;
using System.Text.Json.Serialization;

namespace Mottu.Domain.Entities
{
    /// <summary>
    /// Classe de domínio
    /// </summary>
    public sealed class UserType : BaseEntity
    {
        #region Propriedades

        public string? Name { get; private set; }

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
