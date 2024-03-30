namespace Mottu.Domain.Entities
{
    public sealed class Notification : BaseEntity
    {
        #region Propriedades

        public DateOnly NotificationDate { get; set; }

        //Navigation Properties
        public Order? Order { get; set; }

        // Many-to-many relation
        public IList<NotificatedUser>? NotificatedUsers { get; set; }

        #endregion

        #region Construtores

        public Notification()
        {
        }

        public Notification(Order order, DateOnly notificationDate)
        {
            Validate(order, notificationDate);
        }

        [JsonConstructor]
        public Notification(Guid id, Order order, DateOnly notificationDate)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(order, notificationDate);
        }

        #endregion

        #region Validações

        /// TODO: REAVALIAR SE VAI TER COMO USAR ESSAS VALIDAÇÕES
        
        private void Validate(Order order, DateOnly notificationDate)
        {
            DomainValidation.When(order == null, "Campo 'situação do pedido' é obrigatório");
            DomainValidation.When(notificationDate == DateOnly.MinValue, "Campo 'data da notificação' é obrigatório");

            Order = order;
            NotificationDate = notificationDate;
        }

        #endregion
    }
}
