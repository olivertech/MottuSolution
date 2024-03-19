namespace Mottu.CrossCutting.Messaging
{
    public interface INotificationMessage
    {
        public Guid? Id { get; set; }
        public string? Description { get; set; }
        public DateOnly DateOrder { get; set; }
        public double ValueOrder { get; set; }
        public Guid? StatusOrderId { get; set; }
    }
}
