using Mottu.Domain.Validations;

namespace Mottu.Domain.Entities
{
    public class AppCourier : AppUser
    {
        #region Propriedades

        public CnhType? CnhType { get; private set; }
        public string? Name { get; private set; }
        public string? Cnpj { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public string? Cnh { get; private set; }
        public string? PathCnhImage { get; private set; }

        #endregion

        #region Construtores

        public AppCourier(CnhType cnhType, string name, string cnpj, DateOnly birthDate, string cnh, string pathCnhImage = "")
        {
            Validate(cnhType, name, cnpj, birthDate, cnh, pathCnhImage);
        }

        public AppCourier(int id, CnhType cnhType, string name, string cnpj, DateOnly birthDate, string cnh, string pathCnhImage = "")
        {
            DomainValidation.When(id <= 0, "Id inválido");
            Id = id;
            Validate(cnhType, name, cnpj, birthDate, cnh, pathCnhImage);
        }

        #endregion

        private void Validate(CnhType cnhType, string name, string cnpj, DateOnly birthDate, string cnh, string pathCnhImage = "")
        {
            DomainValidation.When(!cnhType.Name!.Contains('A') && !cnhType.Name!.Contains('B') && !cnhType.Name!.Contains("A+B"), "Campo 'tipo da cnh' só pode ser A, B ou A+B");

            //TODO:
            //IMPLEMENTAR AQUI A PESQUISA DA EXISTENCIA OU NÃO DA CNH E DO CNPJ,
            //POIS NÃO PODE EXISTIR MAIS DE UMA CNH E CNPJ NA BASE

            CnhType = cnhType;
            Name = name;
            Cnpj = cnpj;
            BirthDate = birthDate;
            Cnh = cnh;
            PathCnhImage = pathCnhImage;
        }
    }
}
