namespace DDDTemplate.Application.Authentication.Abstractions;

public interface IRenewAccessTokenCommand
{
    public string RefreshToken { get; set; }
    public Guid UserId { get; set; }
    public string DeviceId { get; set; } 
}