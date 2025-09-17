namespace DDDTemplate.SharedKernel.Abstractions.Services;

public interface IPasswordService
{
    string HashPassword(string password);

    bool VerifyHashedPassword(string password, string hashed);
}