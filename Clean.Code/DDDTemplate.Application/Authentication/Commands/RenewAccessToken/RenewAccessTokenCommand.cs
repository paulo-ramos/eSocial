using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Authentication.Abstractions;
using DDDTemplate.Application.Authentication.Common;

namespace DDDTemplate.Application.Authentication.Commands.RenewAccessToken;

public class RenewAccessTokenCommand : IRenewAccessTokenCommand, ICommand<SigninInfoResultModel>
{
    public string RefreshToken { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string DeviceId { get; set; } = string.Empty;
}