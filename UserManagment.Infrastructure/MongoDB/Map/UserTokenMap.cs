using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.MongoDB.Map
{
    class UserTokenMap
    {
        internal static void Configure()
        {
            BsonClassMap.RegisterClassMap<UserToken>(map =>
            {
                map.AutoMap();
                map.MapIdMember(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

                map.MapMember(x => x.Email).SetIsRequired(true);
                map.MapMember(x => x.AuthorizationCode).SetIsRequired(true);
                map.MapMember(x => x.AuthorizationCodeExpiration).SetIsRequired(true);
                map.MapMember(x => x.RedirectUri).SetIsRequired(true);
                map.MapMember(x => x.IsCanceled).SetIsRequired(true);
            });
        }
    }
}
