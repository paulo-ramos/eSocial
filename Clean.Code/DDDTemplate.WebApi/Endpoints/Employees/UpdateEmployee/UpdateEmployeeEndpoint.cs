using DDDTemplate.Application.Employees.Commands.UpdateEmployee;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.Resources;
using DDDTemplate.FastEndpoint.Endpoints;
using DDDTemplate.WebApi.Enums;
using MediatR;

namespace DDDTemplate.WebApi.Endpoints.Employees.UpdateEmployee;

public class UpdateEmployeeEndpoint(ISender sender) : EndpointBase<UpdateEmployeeCommand>
{
    public override void Configure()
    {
        Post(EndpointRoutes.Employee.Update);
        Description(x =>
            x.WithMetadata(ResourceCode.Employees)
        );

        Permissions(ResourceActions.EditAll, ResourceActions.EditModule, ResourceActions.EditOwner);
    }

    public override async Task HandleAsync(UpdateEmployeeCommand req, CancellationToken ct)
    {
        var result = await sender.Send(req, ct);
        if (result.IsSuccess)
        {
            await SendOkAsync(ct);
            return;
        }

        await SendProblemDetailsAsync(result);
    }
}