using DDDTemplate.Domain.Users;

namespace DDDTemplate.Application.Abstractions.Providers;

public interface IJwtProvider
{
    Task<string> GetAccessTokenAsync(User user, string deviceId);

    Task<string> GetRefreshTokenAsync(User user, string deviceId);

    Task<Guid?> ValidateRefreshTokenAsync(string token, string deviceId);
}