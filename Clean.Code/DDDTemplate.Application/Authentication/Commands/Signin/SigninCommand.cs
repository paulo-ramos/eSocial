using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Authentication.Abstractions;
using DDDTemplate.Application.Authentication.Common;

namespace DDDTemplate.Application.Authentication.Commands.Signin;

public class SigninCommand : ISigninCommand, ICommand<SigninInfoResultModel>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string DeviceId { get; set; } = string.Empty;
}