using InventoryManagementCore.Domain.SeedWork;
using MongoDB.Driver;

namespace InventoryManagementCore.Infrastructure.Services
{
    public interface IDbContext
    {
        AuditedCollection<TEntity> GetAuditedCollection<TEntity>(string? collectionName = null) where TEntity : AuditedEntity;
        IMongoCollection<TEntity> GetCollection<TEntity>(string? collectionName = null) where TEntity : IEntity;
    }
}
