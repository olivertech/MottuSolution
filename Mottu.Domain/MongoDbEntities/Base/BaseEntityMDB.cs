using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mottu.Domain.MongoDbEntities.Base
{
    public class BaseEntityMDB : IEntity
    {
        /// <summary>
        /// Por questões próprias do MongoDb,
        /// se manteve o Id como string
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
