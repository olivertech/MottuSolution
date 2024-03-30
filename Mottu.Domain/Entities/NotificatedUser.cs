namespace Mottu.Domain.Entities
{
    public sealed class NotificatedUser : BaseEntity
    {
        #region Propriedades

        public DateOnly NotificationDate { get; set; }

        //Navigation Properties
        public Guid? UserId { get; set; }
        public AppUser? User { get; private set; }

        public Guid? NotificationId { get; set; }
        public Notification? Notification { get; private set; }

        #endregion

        #region Construtores

        public NotificatedUser()
        {
        }

        public NotificatedUser(AppUser user, Notification notification, DateOnly notificatioDate)
        {
            Validate(user, notification, notificatioDate);
        }

        [JsonConstructor]
        public NotificatedUser(Guid id, AppUser user, Notification notification, DateOnly notificatioDate)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(user, notification, notificatioDate);
        }

        #endregion

        #region Validações

        /// TODO: REAVALIAR SE VAI TER COMO USAR ESSAS VALIDAÇÕES
        
        private void Validate(AppUser user, Notification notification, DateOnly notificatioDate)
        {
            DomainValidation.When(user == null, "campo 'entregador' é obrigatório");
            DomainValidation.When(notification == null, "campo 'notificação' é obrigatório");
            DomainValidation.When(notificatioDate == DateOnly.MinValue, "campo 'data de notificação' é obrigatório");

            User = user;
            Notification = notification;
            NotificationDate = notificatioDate;
        }

        #endregion
    }
}
