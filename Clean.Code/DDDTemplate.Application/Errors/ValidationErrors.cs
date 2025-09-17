using DDDTemplate.SharedKernel.Primitives;

namespace DDDTemplate.Application.Errors;

public class ValidationErrors
{
    public static Error IsRequired => new("ValidationError", "This is required field.");
    public static Error IsFormatWrong => new("ValidationError.IsFormatWrong", "This is wrong format.");
    public static Error IsInvalidData => new("ValidationError.IsInvalidData", "This is invalid data.");

    public static Error MaximumLength(int max) => new("ValidationError.MaximumLength",
        $"This is over maximum length. Max: {max} characters.");

    public static Error GreaterThan(long number) =>
        new("ValidationError.GreaterThan", $"This is must greater than {number}.");

    public static Error NotMach(string matchData) =>
        new("ValidationError.GreaterThan", $"Make source your {matchData} match.");
}