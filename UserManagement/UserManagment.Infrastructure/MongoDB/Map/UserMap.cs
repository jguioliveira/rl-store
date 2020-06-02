using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.MongoDb.Map
{
    class UserMap
    {
        internal static void Configure()
        {
            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);

                map.MapIdMember(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

                map.MapMember(x => x.FirstName).SetIsRequired(true).SetElementName("firstName");
                map.MapMember(x => x.LastName).SetIsRequired(true).SetElementName("lastName");
                map.MapMember(x => x.Email).SetIsRequired(true).SetElementName("email");
                map.MapMember(x => x.Active).SetElementName("active");
                map.MapMember(x => x.Groups).SetElementName("groups");
            });
        }
    }
}
