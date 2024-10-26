using FluentValidation;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Handlers;
using Ramos.eSocial.S1000.Application.Mediator;
using Ramos.eSocial.S1000.Application.Validators;
using Ramos.eSocial.S1000.Domain.Interfaces;
using Ramos.eSocial.S1000.Infrastructure.Repositories;
using Ramos.eSocial.S1000.Shared.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMediator, Mediator>();
builder.Services.AddTransient<ICommandHandler<CreateDadosEmpregadorCommand>, CreateDadosEmpregadorCommandHandler>();
builder.Services.AddTransient<IDadosEmpregadorRepository, DadosEmpregadorRepository>();
builder.Services.AddTransient<IValidator<CreateDadosEmpregadorCommand>, CreateDadosEmpregadorCommandValidator>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPost("/empregador", (CreateDadosEmpregadorCommand command, IMediator mediator) =>
    {
        var result = mediator.Send(command);
        if (!result.Success)
        {
            return Results.BadRequest(new { Success = result.Success, Message = result.Message});
        }

        return Results.Ok(new { Success = result.Success, Message = result.Message });
    })
.WithName("PostDadosEmpregador")
.WithOpenApi();

app.Run();

