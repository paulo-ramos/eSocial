namespace DDDTemplate.Application.Authentication.Options;

public class AuthenticationOptions
{
    public int SignInOtpCodeLifeTime { get; set; }
    public int AccessTokenLifeTime { get; set; }
    public int RefreshTokenLifeTime { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;
}