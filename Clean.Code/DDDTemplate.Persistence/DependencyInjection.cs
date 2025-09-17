using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Domain.Employees;
using DDDTemplate.Domain.GrantedResources;
using DDDTemplate.Domain.Resources;
using DDDTemplate.Domain.UserPermissions;
using DDDTemplate.Domain.Users;
using DDDTemplate.Persistence.Infrashtructure;
using DDDTemplate.Persistence.Interceptors;
using DDDTemplate.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;

namespace DDDTemplate.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<PublishDomainEventsInterceptor>();
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddSingleton<UpdateSoftDeletableEntitiesInterceptor>();

        services.ConfigureOptions<DatabaseOptionsSetup>();

        // If use pool install. must tract IUserClaimService from Scope to Singleton
        services.AddDbContextPool<DbContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(databaseOptions.ConnectionString);
            dataSourceBuilder.EnableDynamicJson();
            dataSourceBuilder.UseJsonNet();
            var dataSource = dataSourceBuilder.Build();

            dbContextOptionsBuilder.UseNpgsql(dataSource,
                optionsBuilder =>
                {
                    if (databaseOptions.MaxRetryCount > 0)
                        optionsBuilder.EnableRetryOnFailure(databaseOptions.MaxRetryCount);

                    optionsBuilder.CommandTimeout(databaseOptions.CommandTimeout);
                }).AddInterceptors(serviceProvider.GetRequiredService<PublishDomainEventsInterceptor>(),
                serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>(),
                serviceProvider.GetRequiredService<UpdateSoftDeletableEntitiesInterceptor>());

            dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
        });

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<DbContext>());
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<DbContext>());

        services.AddScoped<IEmployeeRepository, EmployeeRepsitory>();
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<GrantedResourceRepository>();
        services.AddScoped<IGrantedResourceRepository, CachedGrantedResourceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
        return services;
    }
}