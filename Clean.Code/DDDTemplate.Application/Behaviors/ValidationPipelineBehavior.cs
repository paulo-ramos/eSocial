using FluentValidation;
using DDDTemplate.SharedKernel.Primitives;
using DDDTemplate.SharedKernel.Results;
using MediatR;

namespace DDDTemplate.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var errors = validators
            .Select(x => x.Validate(request))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .Select(x => new Error(x.PropertyName, x.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Length == 0)
            return await next();
        
        return CreateValidationResult<TResponse>(errors)!;
    }
    
    private static TResult? CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
            return (ValidationResult.WithErrors(errors) as TResult)!;

        var validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, [errors]);

        return validationResult as TResult;
    }
}