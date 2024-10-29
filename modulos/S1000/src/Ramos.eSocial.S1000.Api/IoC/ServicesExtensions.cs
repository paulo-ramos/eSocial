using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Handlers;
using Ramos.eSocial.S1000.Application.Mediator;
using Ramos.eSocial.S1000.Application.Validators;
using Ramos.eSocial.S1000.Domain.Interfaces;
using Ramos.eSocial.S1000.Infrastructure.Database;
using Ramos.eSocial.S1000.Infrastructure.Repositories;
using Ramos.eSocial.S1000.Shared.Handlers;

namespace Ramos.eSocial.S1000.Api.IoC;

public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        // Adiciona as opções JSON personalizadas
        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode; 
        });
        
        // Add services to the container.
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        // Add services
        services.AddSingleton<MongoDbContext>();
        services.AddScoped<IDadosEmpregadorRepository, DadosEmpregadorRepository>();
        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<ICommandHandler<CreateDadosEmpregadorCommand>, CreateDadosEmpregadorCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateDadosEmpregadorCommand>, UpdateDadosEmpregadorCommandHandler>();
        services.AddScoped<ICommandHandler<FetchDadosEmpregadorCommand>, FetchDadosEmpregadorCommandHandler>();
        services.AddTransient<IValidator<CreateDadosEmpregadorCommand>, CreateDadosEmpregadorCommandValidator>();
        services.AddTransient<IValidator<UpdateDadosEmpregadorCommand>, UpdateDadosEmpregadorCommandValidator>();
        services.AddTransient<IValidator<FetchDadosEmpregadorCommand>, FetchDadosEmpregadorCommandValidator>();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}