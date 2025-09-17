using System.Text.RegularExpressions;
using DDDTemplate.SharedKernel.Results;

namespace DDDTemplate.SharedKernel.ValueObjects;

public partial class MobileNumber(string value) : ValueObject
{
    public const int MaxLength = 10;

    public string Value { get; } = value;

    public static Result<MobileNumber> Create(string? mobileNumber, bool isRequired = false)
    {
        var value = mobileNumber?.ToLower().Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(value))
            return isRequired
                ? Result.Failure<MobileNumber>(MobileNumberErrors.NullOrEmpty)
                : new MobileNumber(value);

        if (MaxLength > 0 && value.Length > MaxLength)
            return Result.Failure<MobileNumber>(MobileNumberErrors.MaxLength);

        if (!Regex().IsMatch(value))
            return Result.Failure<MobileNumber>(MobileNumberErrors.InvalidFormat);

        return new MobileNumber(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(MobileNumber obj) => obj.Value;

    [GeneratedRegex(@"^0[1-9][0-9]{8}$", RegexOptions.IgnoreCase)]
    private static partial Regex Regex();
}