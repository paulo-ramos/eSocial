using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Mediator;

namespace Ramos.eSocial.S1000.Api.IoC;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPost("/empregador",
                async (CreateDadosEmpregadorCommand command, IMediator mediator, HttpContext context) =>
                {
                    var processKey = context.Items["ProcessKey"];
                    var processDate = context.Items["ProcessDate"];

                    var result = await mediator.Send(command);
                    if (!result.Success)
                    {
                        return Results.BadRequest(new
                        {
                            Success = result.Success, Message = result.Message, ProcessKey = processKey,
                            ProcessDate = processDate
                        });
                    }

                    return Results.Ok(new
                    {
                        Success = result.Success, Message = result.Message, ProcessKey = processKey,
                        ProcessDate = processDate
                    });
                })
            .WithName("PostDadosEmpregador")
            .WithOpenApi();

        app.MapPut("/empregador",
                async (UpdateDadosEmpregadorCommand command, IMediator mediator, HttpContext context) =>
                {
                    var processKey = context.Items["ProcessKey"];
                    var processDate = context.Items["ProcessDate"];

                    var result = await mediator.Send(command);
                    if (!result.Success)
                    {
                        return Results.BadRequest(new
                        {
                            Success = result.Success, Message = result.Message, ProcessKey = processKey,
                            ProcessDate = processDate
                        });
                    }

                    return Results.Ok(new
                    {
                        Success = result.Success, Message = result.Message, ProcessKey = processKey,
                        ProcessDate = processDate
                    });
                })
            .WithName("PutDadosEmpregador")
            .WithOpenApi();

        app.MapGet("/empregador/{nrInsc}", async (IMediator mediator, string nrInsc, HttpContext context) =>
            {
                var processKey = context.Items["ProcessKey"];
                var processDate = context.Items["ProcessDate"];

                var command = new FetchDadosEmpregadorCommand(nrInsc);
                var result = await mediator.Send(command);
                if (!result.Success)
                {
                    return Results.BadRequest(new
                    {
                        Success = result.Success, Message = result.Message, ProcessKey = processKey,
                        ProcessDate = processDate
                    });
                }

                return Results.Ok(new
                {
                    Data = result.Data, Success = result.Success, ProcessKey = processKey, ProcessDate = processDate
                });
            })
            .WithName("GetDadosEmpregador")
            .WithOpenApi();
    }
}