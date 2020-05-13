using BasicAuthentication.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasicAuthentication.Domain.Entities
{
    public class PermissionModule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public ObjectId ModuleId { get; set; }

        public Permission Permission { get; set; }
    }
}
