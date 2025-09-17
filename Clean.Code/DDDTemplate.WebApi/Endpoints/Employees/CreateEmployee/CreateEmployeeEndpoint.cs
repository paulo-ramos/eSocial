using DDDTemplate.Application.Employees.Commands.CreateEmployee;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.Resources;
using DDDTemplate.FastEndpoint.Endpoints;
using DDDTemplate.WebApi.Enums;
using MediatR;

namespace DDDTemplate.WebApi.Endpoints.Employees.CreateEmployee;

public class CreateEmployeeEndpoint(ISender sender) : EndpointBase<CreateEmployeeCommand, CreateEmployeeCommandResult>
{
    public override void Configure()
    {
        Post(EndpointRoutes.Employee.Create);
        Description(x =>
            x.WithMetadata(ResourceCode.Employees)
        );

        Permissions(ResourceActions.AddAll, ResourceActions.AddModule, ResourceActions.AddOwner);
    }

    public override async Task HandleAsync(CreateEmployeeCommand req, CancellationToken ct)
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