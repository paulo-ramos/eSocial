using DDDTemplate.Application.Authentication.Commands.Signin;
using DDDTemplate.Application.Authentication.Common;
using DDDTemplate.FastEndpoint.Endpoints;
using DDDTemplate.WebApi.Enums;
using MapsterMapper;
using MediatR;

namespace DDDTemplate.WebApi.Endpoints.Authentication.Signin;

public class SigninEndpoint(ISender sender, IMapper mapper) : EndpointBase<SigninRequest, SigninInfoResultModel>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post(EndpointRoutes.Authentication.Signin);
    }

    public override async Task HandleAsync(SigninRequest req, CancellationToken ct)
    {
        var command = mapper.Map<SigninCommand>(req);
        var result = await sender.Send(command, ct);
        if (result.IsSuccess)
        {
            await SendOkAsync(result.Value, ct);
            return;
        }

        await SendProblemDetailsAsync(result);
    }
}