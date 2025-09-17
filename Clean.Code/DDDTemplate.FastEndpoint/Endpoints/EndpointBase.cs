using FastEndpoints;
using DDDTemplate.SharedKernel.Abstractions;
using DDDTemplate.SharedKernel.Primitives;
using DDDTemplate.SharedKernel.Results;
using Microsoft.AspNetCore.Http;

namespace DDDTemplate.FastEndpoint.Endpoints;

public class EndpointBase<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
{
    protected Task SendProblemDetailsAsync(Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException();

        if (result is IValidationResult validationResult)
            return SendResultAsync(CreateProblemDetails("Validation Error", StatusCodes.Status400BadRequest,
                result.Error, validationResult.Errors));

        return SendResultAsync(CreateProblemDetails(result.Error.Description, StatusCodes.Status400BadRequest,
            result.Error));
    }

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error,
        IEnumerable<Error>? errors = null) =>
        new()
        {
            Status = status,
            Instance = title,
            TraceId = error.Code,
            Errors = errors?.Select(x => new ProblemDetails.Error
            {
                Code = x.Code,
                Name = x.Code,
                Reason = x.Description,
            }).ToList() ?? []
        };
}

public class EndpointBase<TRequest> : EndpointBase<TRequest, EmptyResponse> where TRequest : notnull;