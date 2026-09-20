using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Interfaces
{
    public record RefreshTokenResult(bool Succeeded, string? AccessToken, string? RefreshToken, string? Error);

    public interface IRefreshTokenService
    {
        Task<string> GenerateAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<RefreshTokenResult> RotateAsync(string rawToken, CancellationToken cancellationToken = default);
        Task RevokeAllForUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
