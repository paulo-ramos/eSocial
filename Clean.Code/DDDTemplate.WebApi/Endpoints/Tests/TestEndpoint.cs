using FastEndpoints;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.Resources;
using DDDTemplate.FastEndpoint.Endpoints;
using DDDTemplate.WebApi.Enums;

namespace DDDTemplate.WebApi.Endpoints.Tests;

public class TestEndpoint : EndpointWithoutRequestBase<object>
{
    public override void Configure()
    {
        Get(EndpointRoutes.Test.Get);
        Description(x =>
            x.WithMetadata(ResourceCode.Employees)
        );

        Permissions(ResourceActions.AccessAll, ResourceActions.AccessModule, ResourceActions.AccessOwner);
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        await SendOkAsync(new { Ok = "ok" }, ct);
    }
}