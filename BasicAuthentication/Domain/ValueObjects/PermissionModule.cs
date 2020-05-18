using MongoDB.Bson;

namespace BasicAuthentication.Domain.ValueObjects
{
    public class PermissionModule
    {
        public ObjectId ModuleId { get; set; }

        public Permission Permission { get; set; }
    }
}
