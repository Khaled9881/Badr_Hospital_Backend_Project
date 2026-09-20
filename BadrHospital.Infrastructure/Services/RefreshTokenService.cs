using BadrHospital.Application.Common;
using BadrHospital.Application.Interfaces;
using BadrHospital.Infrastructure.Identity;
using HospitalManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace BadrHospital.Infrastructure.Services
{
    public class RefreshTokenService(
        ApplicationDbContext dbContext,
        IIdentityService identityService,
        IJWTService jwtService,
        IConfiguration config) : IRefreshTokenService
    {
        private int ExpiryDays => int.Parse(config["Jwt:RefreshTokenExpiryDays"] ?? "7");

        public async Task<string> GenerateAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var rawToken = GenerateSecureToken();

            var entity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TokenHash = Hash(rawToken),
                ExpiresAt = DateTime.UtcNow.AddDays(ExpiryDays)
            };

            dbContext.RefreshTokens.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return rawToken; // raw value goes to the client; only the hash is stored
        }

        public async Task<RefreshTokenResult> RotateAsync(string rawToken, CancellationToken cancellationToken = default)
        {
            var hash = Hash(rawToken);
            var existing = await dbContext.RefreshTokens
                .FirstOrDefaultAsync(x => x.TokenHash == hash, cancellationToken);

            if (existing == null)
                return new RefreshTokenResult(false, null, null, "Invalid refresh token.");

            // Reuse of an already-revoked token = theft signal -> nuke every session for this user
            if (existing.RevokedAt != null)
            {
                await RevokeAllForUserAsync(existing.UserId, cancellationToken);
                return new RefreshTokenResult(false, null, null, "Token reuse detected. All sessions revoked.");
            }

            if (existing.ExpiresAt < DateTime.UtcNow)
                return new RefreshTokenResult(false, null, null, "Refresh token expired.");

            // Rotate: revoke the old one, issue a new one, link them
            var newRawToken = GenerateSecureToken();
            var newEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = existing.UserId,
                TokenHash = Hash(newRawToken),
                ExpiresAt = DateTime.UtcNow.AddDays(ExpiryDays)
            };

            existing.RevokedAt = DateTime.UtcNow;
            existing.ReplacedByTokenId = newEntity.Id;

            dbContext.RefreshTokens.Add(newEntity);
            await dbContext.SaveChangesAsync(cancellationToken);

            var roles = await identityService.GetUserRolesByIdAsync(existing.UserId.ToString());

            var email = await identityService.GetUserEmailByIdAsync(existing.UserId.ToString()); // add this method if missing

            var accessToken = jwtService.CreateToken(new TokenRequest
            {
                UserId = existing.UserId.ToString(),
                Email = email,
                Roles = roles
            });

            return new RefreshTokenResult(true, accessToken, newRawToken, null);
        }

        public async Task RevokeAllForUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var tokens = await dbContext.RefreshTokens
                .Where(x => x.UserId == userId && x.RevokedAt == null)
                .ToListAsync(cancellationToken);

            foreach (var token in tokens)
                token.RevokedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        private static string GenerateSecureToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }

        private static string Hash(string token)
        {
            var bytes = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(bytes);
        }

        // I forgot to mention Refresh Token Serivce Added in the last committ
    }
}
