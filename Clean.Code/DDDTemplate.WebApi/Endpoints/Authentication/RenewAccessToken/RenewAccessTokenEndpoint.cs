using DDDTemplate.Application.Authentication.Commands.RenewAccessToken;
using DDDTemplate.Application.Authentication.Common;
using DDDTemplate.FastEndpoint.Endpoints;
using DDDTemplate.WebApi.Enums;
using MapsterMapper;
using MediatR;

namespace DDDTemplate.WebApi.Endpoints.Authentication.RenewAccessToken;

public class RenewAccessTokenEndpoint(ISender sender, IMapper mapper)
    : EndpointBase<RenewAccessTokenRequest, SigninInfoResultModel>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post(EndpointRoutes.Authentication.RenewAccessToken);
    }

    public override async Task HandleAsync(RenewAccessTokenRequest req, CancellationToken ct)
    {
        var command = mapper.Map<RenewAccessTokenCommand>(req);
        var result = await sender.Send(command, ct);
        if (result.IsSuccess)
        {
            await SendOkAsync(result.Value, ct);
            return;
        }

        await SendProblemDetailsAsync(result);
    }
}