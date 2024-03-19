namespace Mottu.Api.Classes
{
    /// <summary>
    /// Classe que define o padrão de mensagem 
    /// a ser enviada para o RabbitMQ.
    /// Todas as mensagens associadas a notificação
    /// de pedidos, enviadas para a fila
    /// do RabbitMQ, deverão seguir esse padrão.
    /// </summary>
    public class NotificationMessage
    {
        public string? Description { get; set; }
        public DateOnly DateOrder { get; set; }
        public double ValueOrder { get; set; }
        public Guid? StatusOrderId { get; set; }
    }
}
