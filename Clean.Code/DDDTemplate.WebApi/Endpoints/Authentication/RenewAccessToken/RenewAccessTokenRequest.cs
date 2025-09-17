using FastEndpoints;
using DDDTemplate.Application.Authentication.Abstractions;
using DDDTemplate.SharedKernel.Enums;

namespace DDDTemplate.WebApi.Endpoints.Authentication.RenewAccessToken;

public class RenewAccessTokenRequest : IRenewAccessTokenCommand
{
    [FromHeader(HttpHeaders.XDeviceId)] public string DeviceId { get; set; } = string.Empty;
    [FromHeader(HttpHeaders.XUserId)] public Guid UserId { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
}