using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.MongoDb.Map
{
    class ModuleMap
    {
        internal static void Configure()
        {
            BsonClassMap.RegisterClassMap<Module>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);

                map.MapIdMember(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

                map.MapMember(x => x.Name).SetIsRequired(true).SetElementName("name");
                map.MapMember(x => x.Active).SetIsRequired(false).SetElementName("active");
            });
        }

    }
}
