namespace DDDTemplate.Application.Authentication.Abstractions;

public interface ISigninCommand
{
    public string Username { get; set; } 
    public string Password { get; set; } 
    public string DeviceId { get; set; } 
}