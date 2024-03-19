using Mottu.Domain.Entities.Base;
using Mottu.Domain.Validations;

namespace Mottu.Domain.Entities
{
    public sealed class Consumer : BaseEntity
    {
        #region Propriedades

        public string? Name { get; private set; }
        public string? Address { get; private set; }
        public string? State { get; private set; }
        public string? City { get; private set; }
        public string? Neighborhood { get; private set; }
        public string? ZipCode { get; private set; }

        #endregion

        #region Construtores

        public Consumer(string name, string address, string state, string city, string neighborhood, string zipcode)
        {
            Validate(name, address, state, city, neighborhood, zipcode);
        }

        public Consumer(int id, string name, string address, string state, string city, string neighborhood, string zipcode)
        {
            DomainValidation.When(id <= 0, "Id inválido");
            Id = id;
            Validate(name, address, state, city, neighborhood, zipcode);
        }

        #endregion

        private void Validate(string name, string address, string state, string city, string neighborhood, string zipcode)
        {
            DomainValidation.When(name == null, "campo 'nome' é obrigatório");

            Name = name;
            Address = address;
            State = state;
            City = city;
            Neighborhood = neighborhood;
            ZipCode = zipcode;
        }
    }
}
