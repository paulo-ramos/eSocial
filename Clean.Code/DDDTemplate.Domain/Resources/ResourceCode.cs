using System.Text.RegularExpressions;
using DDDTemplate.SharedKernel.Results;
using DDDTemplate.SharedKernel.ValueObjects;

namespace DDDTemplate.Domain.Resources;

public partial class ResourceCode(string value) : ValueObject
{
    public const int MaxLength = 100;
    public static readonly ResourceCode Profile = new("PROFILE");
    public static readonly ResourceCode Employees = new("EMPLOYEES");
    public static readonly ResourceCode Customers = new("CUSTOMERS");


    public string Value { get; } = value;

    public static Result<ResourceCode> Create(string? resourceCode)
    {
        var value = resourceCode?.ToUpper().Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(value))
            Result.Failure<ResourceCode>(ResourceCodeErrors.NullOrEmpty);

        if (MaxLength > 0 && value.Length > MaxLength)
            Result.Failure<ResourceCode>(ResourceCodeErrors.MaxLength);

        if (!Regex().IsMatch(value))
            Result.Failure<ResourceCode>(ResourceCodeErrors.InvalidFormat);

        return new ResourceCode(value);
    }

    public static implicit operator string(ResourceCode obj) => obj.Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    [GeneratedRegex(@"^[a-zA-Z0-9\-_\.]{1,100}$", RegexOptions.IgnoreCase)]
    private static partial Regex Regex();
}