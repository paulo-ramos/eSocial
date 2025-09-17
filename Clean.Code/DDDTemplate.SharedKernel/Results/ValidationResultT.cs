using DDDTemplate.SharedKernel.Abstractions;
using DDDTemplate.SharedKernel.Primitives;

namespace DDDTemplate.SharedKernel.Results;

public class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    public Error[] Errors { get; }
    
    private ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError)
        => Errors = errors;

    public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
}