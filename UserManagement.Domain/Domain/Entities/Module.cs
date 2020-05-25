using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasicAuthentication.Domain.Entities
{
    public class Module
    {
        public Module(string name, bool active)
        {
            Name = name;
            Active = active;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; }

        [BsonElement("active")]
        public bool Active { get; private set; }
    }
}
