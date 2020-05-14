using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasicAuthentication.Domain.Entities
{
    public class Module
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }
    }
}
