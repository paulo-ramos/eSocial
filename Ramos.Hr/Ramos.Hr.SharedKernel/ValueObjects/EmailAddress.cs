using System.Text.RegularExpressions;
using Ramos.Hr.SharedKernel.Results;

namespace Ramos.Hr.SharedKernel.ValueObjects;

public partial class EmailAddress(string value) : ValueObject
{
    public const int MaxLength = 200;

    public string Value { get; } = value;

    public static Result<EmailAddress> Create(string? emailAddress, bool isRequired = false)
    {
        var value = emailAddress?.ToLower().Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(value))
            return isRequired
                ? Result.Failure<EmailAddress>(EmailAddressErrors.NullOrEmpty)
                : new EmailAddress(value);

        if (MaxLength > 0 && value.Length > MaxLength)
            return Result.Failure<EmailAddress>(EmailAddressErrors.MaxLength);

        if (!Regex().IsMatch(value))
            return Result.Failure<EmailAddress>(EmailAddressErrors.InvalidFormat);

        return new EmailAddress(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(EmailAddress obj) => obj.Value;

    [GeneratedRegex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", RegexOptions.IgnoreCase)]
    private static partial Regex Regex();
}