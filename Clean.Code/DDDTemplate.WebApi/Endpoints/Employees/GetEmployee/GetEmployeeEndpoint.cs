using DDDTemplate.Application.Employees.Queries.GetEmployee;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.Resources;
using DDDTemplate.FastEndpoint.Endpoints;
using DDDTemplate.WebApi.Enums;
using MediatR;

namespace DDDTemplate.WebApi.Endpoints.Employees.GetEmployee;

public class GetEmployeeEndpoint(ISender sender) : EndpointBase<GetEmployeeQuery, GetEmployeeQueryResult>
{
    public override void Configure()
    {
        Get(EndpointRoutes.Employee.Get);
        Description(x =>
            x.WithMetadata(ResourceCode.Employees)
        );

        Permissions(ResourceActions.AccessAll, ResourceActions.AccessModule, ResourceActions.AccessOwner);
    }

    public override async Task HandleAsync(GetEmployeeQuery req, CancellationToken ct)
    {
        var result = await sender.Send(req, ct);
        if (result.IsSuccess)
        {
            await SendOkAsync(result.Value, ct);
            return;
        }

        await SendProblemDetailsAsync(result);
    }
}