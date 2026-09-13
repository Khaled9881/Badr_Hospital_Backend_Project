namespace HospitalManagementSystem.Domain.Common
{
    /// <summary>
    /// Marker for entities that are soft-deleted (flagged, not physically removed).
    /// Picked up automatically by ApplicationDbContext via reflection - implement
    /// this on an entity and it gets a global query filter + SaveChanges interception
    /// for free, no per-entity config needed.
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
