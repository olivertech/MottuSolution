using Mottu.Domain.Entities.Base;
using Mottu.Domain.Validations;
using System.Text.Json.Serialization;

namespace Mottu.Domain.Entities
{
    public sealed class DeliveredOrder : BaseEntity
    {
        #region Propriedades

        public DateOnly? DeliveredDate { get; set; }

        //Navigation Properties
        public Guid UserId { get; set; }
        public AppUser? User { get; private set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; private set; }

        #endregion

        #region Construtores

        public DeliveredOrder()
        {
        }

        public DeliveredOrder(AppUser user, Order order, DateOnly acceptedDate)
        {
            Validate(user, order, acceptedDate);
        }

        [JsonConstructor]
        public DeliveredOrder(Guid id, AppUser user, Order order, DateOnly acceptedDate)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(user, order, acceptedDate);
        }

        #endregion

        #region Validações

        /// TODO: REAVALIAR SE VAI TER COMO USAR ESSAS VALIDAÇÕES
        private void Validate(AppUser user, Order order, DateOnly deliveredDate)
        {
            DomainValidation.When(user == null, "campo 'entregador' é obrigatório");
            DomainValidation.When(order == null, "campo 'pedido' é obrigatório");
            DomainValidation.When(deliveredDate == DateOnly.MinValue, "campo 'data de entrega do pedido' é obrigatório");

            User = user;
            Order = order;
            DeliveredDate = deliveredDate;
        }

        #endregion
    }
}
