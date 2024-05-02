using Mottu.Domain.EntitiesMDB.Base;

namespace Mottu.Domain.EntitiesMDB
{
    public sealed class AppUserMDB : BaseEntityMDB
    {
        #region Propriedades

        //Propriedades comuns a todos os tipos de usuários
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }

        //Propriedades compartilhadas por mais de um tipo de usuário
        public string? Cnpj { get; set; }
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

        public Guid UserTypeId { get; set; }

        //Navigation Properties
        public UserType? UserType { get; set; }
        public CnhType? CnhType { get; set; }

        // Many-to-many relation
        public IList<NotificatedUser>? NotificatedUsers { get; set; }
        public IList<AcceptedOrder>? AcceptedOrders { get; set; }
        public IList<DeliveredOrder>? DeliveredOrders { get; set; }

        #endregion
    }
}
