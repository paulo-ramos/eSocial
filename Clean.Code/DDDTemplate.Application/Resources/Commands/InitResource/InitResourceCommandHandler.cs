using System.Text;
using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Domain.Resources;
using DDDTemplate.SharedKernel.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DDDTemplate.Application.Resources.Commands.InitResource;

public class InitResourceCommandHandler(
    ILogger<InitResourceCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IResourceRepository repository)
    : ICommandHandler<InitResourceCommand>
{
    public async Task<Result> Handle(InitResourceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Databases",
                "Resources.json");
            if (!File.Exists(path))
                return Result.Failure(ResourceErrors.NotFound);

            var resources = JsonConvert.DeserializeObject<Resource[]>(
                await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken));
            if (resources is null || resources.Length == 0)
                return Result.Success();

            foreach (var resource in resources)
            {
                var entity = await repository.GetByIdAsync(resource.Id);
                if (entity is null)
                    repository.Insert(resource);
                else
                    entity.Update(resource.Code, resource.Name, resource.Description, resource.IsBlocked);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        return Result.Failure(ResourceErrors.ErrorInProcessing);
    }
}