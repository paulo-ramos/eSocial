using System.Text.RegularExpressions;
using DDDTemplate.SharedKernel.Abstractions.Services;
using DDDTemplate.SharedKernel.Results;
using DDDTemplate.SharedKernel.ValueObjects;

namespace DDDTemplate.Domain.Users;

public partial class Password(string value) : ValueObject
{
    public const int MaxLength = 20;
    public const int MaxDbLength = 250;
    public const int MinLength = 6;

    public string Value { get; } = value;

    public static string Format(string? password)
        => password?.ToLower().Trim() ?? string.Empty;

    public static Result<Password> Create(string? password, IPasswordService passwordService, bool isRequired = false)
    {
        var value = Format(password);
        if (string.IsNullOrEmpty(value))
            return isRequired
                ? Result.Failure<Password>(PasswordErrors.NullOrEmpty)
                : new Password(string.Empty);

        if (MaxLength > 0 && value.Length > MaxLength)
            Result.Failure<Password>(PasswordErrors.MaxLength);

        if (MinLength > 0 && value.Length < MinLength)
            Result.Failure<Password>(PasswordErrors.MinLength);

        if (!Regex().IsMatch(value))
            Result.Failure<Password>(PasswordErrors.InvalidFormat);

        return new Password(passwordService.HashPassword(value));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(Password obj) => obj.Value;

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,20})", RegexOptions.IgnoreCase)]
    private static partial Regex Regex();
}