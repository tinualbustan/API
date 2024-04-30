using InventoryManagementCore.Domain.SeedWork;
using MongoDB.Driver;

namespace InventoryManagementCore.Infrastructure.Services
{
    public class AuditedCollection<TEntity> : IAuditedCollection<TEntity> where TEntity : AuditedEntity
    {
        private readonly IMongoCollection<TEntity> collection;

        public AuditedCollection(IMongoCollection<TEntity> collection)
        {
            this.collection = collection;
        }
        public Task InsertOneAsync(TEntity entity)
        {
            //TODO Audit
            return collection.InsertOneAsync(entity);
        }
        public Task UpdateOneAsync(TEntity entity)
        {
            //TODO Audit
            return collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }
        public Task DeleteOneAsync(string Id)
        {
            //TODO Audit
            return collection.DeleteOneAsync(e => e.Id == Id);
        }
    }
}
