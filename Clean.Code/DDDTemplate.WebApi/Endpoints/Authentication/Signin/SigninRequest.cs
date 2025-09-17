using FastEndpoints;
using DDDTemplate.Application.Authentication.Abstractions;
using DDDTemplate.SharedKernel.Enums;

namespace DDDTemplate.WebApi.Endpoints.Authentication.Signin;

public class SigninRequest : ISigninCommand
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [FromHeader(HttpHeaders.XDeviceId)] public string DeviceId { get; set; } = string.Empty;
}