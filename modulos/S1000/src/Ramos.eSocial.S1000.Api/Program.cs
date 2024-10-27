using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Handlers;
using Ramos.eSocial.S1000.Application.Mediator;
using Ramos.eSocial.S1000.Application.Validators;
using Ramos.eSocial.S1000.Domain.Interfaces;
using Ramos.eSocial.S1000.Infrastructure.Database;
using Ramos.eSocial.S1000.Infrastructure.Middleware;
using Ramos.eSocial.S1000.Infrastructure.Repositories;
using Ramos.eSocial.S1000.Shared.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Adiciona as opções JSON personalizadas
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
//    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    options.JsonSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode; // Adiciona isso para ignorar campos extras
});


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
builder.Services.AddScoped<ICommandHandler<UpdateDadosEmpregadorCommand>, UpdateDadosEmpregadorCommandHandler>();
builder.Services.AddScoped<ICommandHandler<FetchDadosEmpregadorCommand>, FetchDadosEmpregadorCommandHandler>();
builder.Services.AddTransient<IValidator<CreateDadosEmpregadorCommand>, CreateDadosEmpregadorCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateDadosEmpregadorCommand>, UpdateDadosEmpregadorCommandValidator>();
builder.Services.AddTransient<IValidator<FetchDadosEmpregadorCommand>, FetchDadosEmpregadorCommandValidator>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<RequestIdMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/empregador", async (CreateDadosEmpregadorCommand command, IMediator mediator, HttpContext context) =>
    {
        var processKey = context.Items["ProcessKey"];
        var processDate = context.Items["ProcessDate"];
        
        var result = await mediator.Send(command);
        if (!result.Success)
        {
            return Results.BadRequest(new { Success = result.Success, Message = result.Message, ProcessKey = processKey, ProcessDate = processDate});
        }

        return Results.Ok(new { Success = result.Success, Message = result.Message, ProcessKey = processKey, ProcessDate = processDate });
    })
.WithName("PostDadosEmpregador")
.WithOpenApi();



app.MapPut("/empregador", async (UpdateDadosEmpregadorCommand command, IMediator mediator, HttpContext context) =>
    {
        var processKey = context.Items["ProcessKey"];
        var processDate = context.Items["ProcessDate"];
        
        var result = await mediator.Send(command);
        if (!result.Success)
        {
            return Results.BadRequest(new { Success = result.Success, Message = result.Message, ProcessKey = processKey, ProcessDate = processDate});
        }

        return Results.Ok(new { Success = result.Success, Message = result.Message, ProcessKey = processKey, ProcessDate = processDate });
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
            return Results.BadRequest(new { Success = result.Success, Message = result.Message, ProcessKey = processKey, ProcessDate = processDate});
        }

        return Results.Ok(new { Data = result.Data, Success = result.Success, ProcessKey = processKey, ProcessDate = processDate });
    })
    .WithName("GetDadosEmpregador")
    .WithOpenApi();


app.Run();

