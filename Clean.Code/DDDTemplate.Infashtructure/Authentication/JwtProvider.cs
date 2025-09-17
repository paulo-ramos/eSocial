using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DDDTemplate.Application.Abstractions.Providers;
using DDDTemplate.Application.Authentication.Options;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.Abstractions.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DDDTemplate.Infashtructure.Authentication;

public class JwtProvider(IOptions<AuthenticationOptions> options, ICacheService cacheService) : IJwtProvider
{
    private readonly AuthenticationOptions _options = options.Value;

    private static string GetRefreshTokenCacheKey(string idUser, string deviceId)
        => $"refresh_token:{idUser},{deviceId}";

    public Task<string> GetAccessTokenAsync(User user, string deviceId)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, user.Username),
            new(JwtRegisteredClaimNames.NameId, deviceId),
        };

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.Now.AddMinutes(_options.AccessTokenLifeTime),
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256));

        return Task.FromResult(new JwtSecurityTokenHandler()
            .WriteToken(token));
    }

    public async Task<string> GetRefreshTokenAsync(User user, string deviceId)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        var expiryDate = DateTime.Now.AddMinutes(_options.RefreshTokenLifeTime);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            expiryDate,
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.PrivateKey)),
                SecurityAlgorithms.HmacSha256));

        var tokenResult = new JwtSecurityTokenHandler()
            .WriteToken(token);

        if (!string.IsNullOrEmpty(tokenResult))
            await cacheService.SetAsync(GetRefreshTokenCacheKey(user.Id.ToString(), deviceId), tokenResult, expiryDate);

        return tokenResult;
    }

    public async Task<Guid?> ValidateRefreshTokenAsync(string token, string deviceId)
    {
        try
        {
            new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.PrivateKey))
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var value = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(value))
                return null;

            var cachedToken = await cacheService.GetAsync<string?>(GetRefreshTokenCacheKey(value, deviceId));
            if (string.IsNullOrEmpty(cachedToken) || !token.Equals(cachedToken))
                return null;

            return Guid.Parse(value);
        }
        catch
        {
            return null;
        }
    }
}