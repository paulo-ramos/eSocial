using FastEndpoints;
using DDDTemplate.Application;
using DDDTemplate.Infashtructure;
using DDDTemplate.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);

builder.Services.AddResponseCaching()
    .AddCors(options =>
        options
            .AddDefaultPolicy(policy =>
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Content-Disposition")))
    .AddResponseCompression().AddFastEndpoints();


var app = builder.Build();

app
    .UseResponseCaching()
    .UseCors()
    .UseAuthentication()
    .UseAuthorization()
    .UseResponseCompression()
    .UseFastEndpoints(c => { c.Endpoints.RoutePrefix = "api"; });

app.Run();