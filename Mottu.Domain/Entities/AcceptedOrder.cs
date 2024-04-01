namespace Mottu.Domain.Entities
{
    public sealed class AcceptedOrder : BaseEntity
    {
        #region Propriedades

        public DateOnly? AcceptedDate { get; set; }

        //Navigation Properties
        public Guid UserId { get; set; }
        public AppUser? User { get; private set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; private set; }

        #endregion

        #region Construtores

        public AcceptedOrder()
        {
        }

        public AcceptedOrder(AppUser user, Order order, DateOnly acceptedDate)
        {
            Validate(user, order, acceptedDate);
        }

        [JsonConstructor]
        public AcceptedOrder(Guid id, AppUser user, Order order, DateOnly acceptedDate)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(user, order, acceptedDate);
        }

        #endregion

        #region Validações

        /// TODO: REAVALIAR SE VAI TER COMO USAR ESSAS VALIDAÇÕES
        private void Validate(AppUser user, Order order, DateOnly acceptedDate)
        {
            DomainValidation.When(user == null, "campo 'entregador' é obrigatório");
            DomainValidation.When(order == null, "campo 'pedido' é obrigatório");
            DomainValidation.When(acceptedDate == DateOnly.MinValue, "campo 'data de aceite do pedido' é obrigatório");

            User = user;
            Order = order;
            AcceptedDate = acceptedDate;
        }

        #endregion
    }
}
