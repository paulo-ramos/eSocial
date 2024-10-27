using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Handlers;
using Ramos.eSocial.S1000.Application.Mediator;
using Ramos.eSocial.S1000.Application.Validators;
using Ramos.eSocial.S1000.Domain.Interfaces;
using Ramos.eSocial.S1000.Infrastructure.Database;
using Ramos.eSocial.S1000.Infrastructure.Repositories;
using Ramos.eSocial.S1000.Shared.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Registrar o serializador de GUID globalmente
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IDadosEmpregadorRepository, DadosEmpregadorRepository>();
//builder.Services.AddScoped<IPessoaService, PessoaService>(); // Supondo que você tenha um serviço também
builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddScoped<ICommandHandler<CreateDadosEmpregadorCommand>, CreateDadosEmpregadorCommandHandler>();
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

app.MapPost("/empregador", async (CreateDadosEmpregadorCommand command, IMediator mediator) =>
    {
        var result = await mediator.Send(command);
        if (!result.Success)
        {
            return Results.BadRequest(new { Success = result.Success, Message = result.Message});
        }

        return Results.Ok(new { Success = result.Success, Message = result.Message });
    })
.WithName("PostDadosEmpregador")
.WithOpenApi();

app.Run();

