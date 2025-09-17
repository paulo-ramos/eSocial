using DDDTemplate.Application.Data;
using DDDTemplate.Application.Employees.Queries.GetEmployee;
using DDDTemplate.Application.Employees.Queries.GetEmployees;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.Resources;
using DDDTemplate.FastEndpoint.Endpoints;
using DDDTemplate.WebApi.Enums;
using MediatR;

namespace DDDTemplate.WebApi.Endpoints.Employees.GetEmployees;

public class GetEmployeesEndpoint(ISender sender) : EndpointBase<GetEmployeesQuery, PageList<GetEmployeesQueryResult>>
{
    public override void Configure()
    {
        Get(EndpointRoutes.Employee.List);
        Description(x =>
            x.WithMetadata(ResourceCode.Employees)
        );

        Permissions(ResourceActions.AccessAll, ResourceActions.AccessModule, ResourceActions.AccessOwner);
    }

    public override async Task HandleAsync(GetEmployeesQuery req, CancellationToken ct)
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