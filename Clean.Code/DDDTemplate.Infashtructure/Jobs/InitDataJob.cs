using System.Reflection;
using DDDTemplate.Application.Attributes;
using DDDTemplate.SharedKernel.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DDDTemplate.Infashtructure.Jobs;

public class InitDataJob(ILogger<InitDataJob> logger, ISender sender, IConfiguration configuration) : IJob
{
    private List<TypeRecord> _types = null!;

    private record TypeRecord(Type Type, int SortIndex);

    public async Task Execute(IJobExecutionContext context)
    {
        if (!configuration.GetSection("BackgroundJobs:InitData").GetValue<bool>("Enable"))
            return;

        try
        {
            _types = [];
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName is not null && x.FullName.StartsWith("DDDTemplate."));
            foreach (var assembly in assemblies)
                await ProcessAssembly(assembly);
            
            if (_types.Count == 0)
            {
                logger.LogInformation("No init data job!!!");
                return;
            }
            
            _types = _types.OrderBy(x => x.SortIndex).ToList();
            foreach (var type in _types)
                await ProcessType(type.Type);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }

    private Task ProcessAssembly(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            var attribute = type.GetCustomAttribute<InitDataCommandAttribute>();
            if (attribute is null)
                continue;

            _types.Add(new TypeRecord(type, attribute.SortIndex));
        }

        return Task.CompletedTask;
    }

    private async Task ProcessType(Type type)
    {
        try
        {
            var command = Activator.CreateInstance(type);
            if (command is null)
                return;

            logger.LogInformation($"Execute init command {type.FullName}...");
            if (await sender.Send(command) is not Result result)
            {
                logger.LogError("Execute init command failed: Parse result failed");
                return;
            }

            if (result.IsFailure)
            {
                logger.LogError($"Execute init command failed: {result.Error.Code} - {result.Error.Description}");
                return;
            }

            logger.LogInformation("Execute init command success!!!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }
}