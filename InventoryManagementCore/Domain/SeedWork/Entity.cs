namespace InventoryManagementCore.Domain.SeedWork
{
    public class Entity : IEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
    }
}
