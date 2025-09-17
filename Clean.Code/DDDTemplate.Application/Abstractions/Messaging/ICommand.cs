using DDDTemplate.SharedKernel.Results;
using MediatR;

namespace DDDTemplate.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
    
}