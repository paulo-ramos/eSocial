using DDDTemplate.Application.Abstractions.Providers;
using DDDTemplate.Application.Abstractions.Services;
using DDDTemplate.Infashtructure.Authentication;
using DDDTemplate.Infashtructure.Caches;
using DDDTemplate.Infashtructure.Jobs;
using DDDTemplate.Infashtructure.Providers;
using DDDTemplate.Infashtructure.Services;
using DDDTemplate.SharedKernel.Abstractions.Providers;
using DDDTemplate.SharedKernel.Abstractions.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using StackExchange.Redis;

namespace DDDTemplate.Infashtructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IClaimsTransformation, ClaimsTransformation>();
        
        // For caching
        services.Configure<CachingSettings>(options => configuration.GetSection("Caching").Bind(options));
        services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<CachingSettings>>().Value;
            return ConnectionMultiplexer.Connect(settings.ConnectionString);
        });

        services.AddScoped<ICacheService, CacheService>();

        // Add Quartz
        services.AddQuartz();

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
            options.AwaitApplicationStarted = true;
        });

        services.ConfigureOptions<BackgroundJobsSetup>();

        // Add Authentication + Authorization
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerOptionsSetup>();
        services.AddHttpContextAccessor();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

        services.AddAuthorizationBuilder()
            .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new OperationAuthorizationRequirement())
                .Build());
        return services;
    }
}