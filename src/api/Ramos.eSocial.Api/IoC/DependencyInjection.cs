using Ramos.eSocial.S1000.IoC;
using Ramos.eSocial.Shared.IoC;
using Ramos.eSocial.Shared.Util.Interface;

namespace Ramos.eSocial.Api.IoC;

using Microsoft.Extensions.DependencyInjection;
using Ramos.eSocial.S1000.IoC;
using Ramos.eSocial.Shared.Util;

public static class DependencyInjection
{
    public static IServiceCollection AddModules(this IServiceCollection services)
    {
        services.AddSharedServices();
        services.AddS1000Module();

        return services;
    }
}