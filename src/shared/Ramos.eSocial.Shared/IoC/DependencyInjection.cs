using Ramos.eSocial.Shared.Util.Interface;
using Microsoft.Extensions.DependencyInjection;
using Ramos.eSocial.Shared.Util;

namespace Ramos.eSocial.Shared.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IEventIdGenerator, EventIdGenerator>();

        return services;
    }
}