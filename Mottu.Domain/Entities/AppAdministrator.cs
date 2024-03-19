using Mottu.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Domain.Entities
{
    public class AppAdministrator : AppUser
    {
        #region Propriedades

        public string? Name { get; private set; }
        public string? Cnpj { get; private set; }
        public DateOnly BirthDate { get; private set; }

        #endregion

        #region Construtores

        public AppAdministrator(string name, string cnpj, DateOnly birthDate)
        {
            Validate(name, cnpj, birthDate);
        }

        public AppAdministrator(int id, string name, string cnpj, DateOnly birthDate)
        {
            DomainValidation.When(id <= 0, "Id inválido");
            Id = id;
            Validate(name, cnpj, birthDate);
        }

        #endregion

        private void Validate(string name, string cnpj, DateOnly birthDate)
        {
            //TODO:
            //IMPLEMENTAR AQUI A PESQUISA DA EXISTENCIA OU NÃO DA CNH E DO CNPJ,
            //POIS NÃO PODE EXISTIR MAIS DE UMA CNH E CNPJ NA BASE

            Name = name;
            Cnpj = cnpj;
            BirthDate = birthDate;
        }
    }
}
