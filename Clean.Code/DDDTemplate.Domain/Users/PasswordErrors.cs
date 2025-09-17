using DDDTemplate.SharedKernel.Primitives;

namespace DDDTemplate.Domain.Users;

public class PasswordErrors
{
    public static readonly Error NullOrEmpty = new("Password.NullOrEmpty", "Password is null or empty.");

    public static readonly Error InvalidFormat = new("Password.InvalidFormat", "Password format is invalid.");

    public static readonly Error MaxLength = new("Password.MaxLength",
        $"Password length must be less than or equal {Password.MaxLength:D0} characters.");

    public static readonly Error MinLength = new("Password.MinLength",
        $"Password length must be greater than or equal {Password.MinLength:D0} characters.");
}