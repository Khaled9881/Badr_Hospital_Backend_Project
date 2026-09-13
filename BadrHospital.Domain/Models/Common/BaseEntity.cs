namespace HospitalManagementSystem.Domain.Common
{
    /// <summary>
    /// Common base for all domain entities (GUID primary key).
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
