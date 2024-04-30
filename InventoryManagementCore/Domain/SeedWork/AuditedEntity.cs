﻿namespace InventoryManagementCore.Domain.SeedWork
{
    public class AuditedEntity : Entity
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

    }
}
