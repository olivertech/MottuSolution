using Mottu.Domain.Entities.Base;
using Mottu.Domain.Validations;

namespace Mottu.Domain.Entities
{
    public sealed class AppUser : BaseEntity
    {
        #region Propriedades

        //Propriedades comuns a todos os tipos de usuários
        public string? Name { get; private set; }
        public string? Login { get; private set; }
        public string? Password { get; set; }

        //Propriedades compartilhadas por mais de um tipo de usuário
        public string? Cnpj { get; private set; }
        public DateOnly? BirthDate { get; private set; }
        public string? Cnh { get; set; }
        public string? PathCnhImage { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelivering { get; set; } = false;

        //Propriedades associadas ao tipo de usuário 'Consumidor'
        public string? Address { get; private set; }
        public string? State { get; private set; }
        public string? City { get; private set; }
        public string? Neighborhood { get; private set; }
        public string? ZipCode { get; private set; }

        //Navigation Properties
        public UserType? UserType { get; set; }
        public CnhType? CnhType { get; set; }

        // Many-to-many relation
        public IList<NotificatedUser>? NotificatedUsers { get; set; }
        public IList<AcceptedOrder>? AcceptedOrders { get; set; }
        public IList<DeliveredOrder>? DeliveredOrders { get; set; }

        #endregion

        #region Construtores

        public AppUser()
        {
        }

        #region Construtores Cadastro de Usuário - Passo 1 (Apenas Login e Password)

        public AppUser(string name, string login, string password, UserType userType)
        {
            Validate(name, login, password, userType);
        }

        public AppUser(Guid id, string name, string login, string password, UserType userType)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(name, login, password, userType);
        }

        #endregion

        #region Construtores Cadastro de Usuário - Passo 2 (Perfil Administrador)

        public AppUser(DateOnly birthDate, CnhType cnhType)
        {
            Validate(birthDate, cnhType);
        }

        public AppUser(Guid id, DateOnly birthDate, CnhType cnhType)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(birthDate, cnhType);
        }

        #endregion

        #region Construtores Cadastro de Usuário - Passo 2 (Perfil Entregador)

        public AppUser(string cnpj, CnhType cnhType, string cnh, string pathCnhImage = "")
        {
            Validate(cnhType, cnpj, cnh, pathCnhImage);
        }

        public AppUser(Guid id, string cnpj, CnhType cnhType, string cnh, string pathCnhImage = "")
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(cnhType, cnpj, cnh, pathCnhImage);
        }

        #endregion

        #endregion

        #region Validações

        /// TODO: REAVALIAR SE VAI TER COMO USAR ESSAS VALIDAÇÕES
        #region Validação Passo 1

        private void Validate(string name, string login, string password, UserType userType)
        {
            DomainValidation.When(name is null || name.Length == 0, "Campo 'nome' é obrigatório");
            DomainValidation.When(login is null || login.Length == 0, "Campo 'login' é obrigatório");
            DomainValidation.When(password is null || password.Length == 0, "Campo 'password' é obrigatório");
            DomainValidation.When(userType is null, "Campo 'tipo do usuário' é obrigatório");

            Name = name!.ToUpper();
            Login = login;
            Password = password;
            UserType = userType;
        }

        #endregion

        #region Validação Passo 2 (Perfil Administrador)

        private void Validate(DateOnly birthDate, CnhType cnhType)
        {
            DomainValidation.When(!cnhType.Name!.Contains("NA"), "Campo 'tipo da cnh' só pode ser 'NA'");

            BirthDate = birthDate;
            Cnpj = "***" ;
            Cnh = "***";
            CnhType = cnhType;
        }

        #endregion

        #region Validação Passo 2 (Perfil Entregador)

        private void Validate(CnhType cnhType, string cnpj, string cnh, string pathCnhImage = "")
        {
            DomainValidation.When(cnh is null || cnh.Length == 0, "Campo 'nome' é obrigatório");
            DomainValidation.When(!cnhType.Name!.Contains('A') && !cnhType.Name!.Contains('B') && !cnhType.Name!.Contains("AB"), "Campo 'tipo da cnh' só pode ser A, B ou AB");
            DomainValidation.When(cnpj is null || cnpj.Length == 0, "Campo 'cnpj' é obrigatório");

            Cnpj = cnpj;
            CnhType = cnhType;
            Cnh = cnh;
            PathCnhImage = pathCnhImage;
        }

        #endregion

        #endregion
    }
}
