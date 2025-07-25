using Ramos.eSocial.S1000.Application.Handlers;
using Ramos.eSocial.S1000.Infrastructure.Repositories;
using Ramos.eSocial.S1000.Infrastructure.Repositories.Inteface;
using Microsoft.Extensions.DependencyInjection;

namespace Ramos.eSocial.S1000.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddS1000Module(this IServiceCollection services)
    {
        services.AddSingleton<IEventoS1000Repository, EventoS1000Repository>();
        services.AddScoped<IncluirEventoS1000Handler>();
        services.AddScoped<AlterarEventoS1000Handler>();
        services.AddScoped<ExcluirEventoS1000Handler>();

        return services;
    }
}