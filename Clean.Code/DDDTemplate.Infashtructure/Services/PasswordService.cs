using DDDTemplate.SharedKernel.Abstractions.Services;

namespace DDDTemplate.Infashtructure.Services;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
       return BCrypt.Net.BCrypt.HashPassword(password, 8);
    }

    public bool VerifyHashedPassword(string password, string hashed)
    {
        return !string.IsNullOrEmpty(password) && BCrypt.Net.BCrypt.Verify(password, hashed);
    }
}