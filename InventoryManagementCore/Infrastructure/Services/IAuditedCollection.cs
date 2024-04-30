using InventoryManagementCore.Domain.SeedWork;

namespace InventoryManagementCore.Infrastructure.Services
{
    public interface IAuditedCollection<TEntity> where TEntity : AuditedEntity
    {
        Task DeleteOneAsync(string Id);
        Task InsertOneAsync(TEntity entity);
        Task UpdateOneAsync(TEntity entity);
    }
}
