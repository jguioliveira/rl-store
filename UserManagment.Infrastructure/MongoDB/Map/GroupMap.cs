using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.MongoDb.Map
{
    class GroupMap
    {
        internal static void Configure()
        {
            BsonClassMap.RegisterClassMap<Group>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapCreator(group => new Group(group.Name));

                map.MapIdMember(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

                map.MapMember(x => x.Name).SetIsRequired(true).SetElementName("name");
                map.MapMember(x => x.PermissionModules).SetIsRequired(true).SetElementName("permissionModules");
            });
        }

    }
}
