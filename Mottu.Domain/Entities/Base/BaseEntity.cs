namespace Mottu.Domain.Entities.Base
{
    public class BaseEntity : IEntity
    {
        /// <summary>
        /// Por questões próprias do MongoDb,
        /// se manteve o Id como string
        /// </summary>
        [Key]
        public Guid? Id { get; set; }
    }
}
