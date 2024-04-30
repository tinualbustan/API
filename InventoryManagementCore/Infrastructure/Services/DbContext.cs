using InventoryManagementCore.Domain.SeedWork;
using MongoDB.Driver;

namespace InventoryManagementCore.Infrastructure.Services
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase database;

        public DbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            IMongoDatabase _database = client.GetDatabase(configuration["MongoDB:DataBaseName"]);
            database = _database;
        }
        public IMongoCollection<TEntity> GetCollection<TEntity>(string? collectionName = null) where TEntity : IEntity
        {
            return database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
        }
        public AuditedCollection<TEntity> GetAuditedCollection<TEntity>(string? collectionName = null) where TEntity : AuditedEntity
        {
            var collection = database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
            return new AuditedCollection<TEntity>(collection);
        }
    }
}
