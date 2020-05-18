using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasicAuthentication.Domain.Entities
{
    public class Module
    {
        public Module()
        {
        }

        public Module(string name, bool active)
        {
            Name = name;
            Active = active;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }
    }
}
