using UserManagement.Domain.Entities;
using MongoDB.Driver;

namespace BasicAuthentication.Infrastructure.Context
{
    public interface IUserDataContext
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<Module> Modules { get; }
        IMongoCollection<Group> Groups { get; }
    }
}