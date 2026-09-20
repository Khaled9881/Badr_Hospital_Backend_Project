using HospitalManagementSystem.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Infrastructure.Identity
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string TokenHash { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedAt { get; set; }
        public Guid? ReplacedByTokenId { get; set; }

        public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;

        public ApplicationUser User { get; set; } = null!;
    }
}
