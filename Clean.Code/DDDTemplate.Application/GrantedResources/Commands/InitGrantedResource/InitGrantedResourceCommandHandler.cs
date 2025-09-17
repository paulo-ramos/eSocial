using System.Text;
using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Domain.GrantedResources;
using DDDTemplate.Domain.Resources;
using DDDTemplate.SharedKernel.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DDDTemplate.Application.GrantedResources.Commands.InitGrantedResource;

public class InitGrantedResourceCommandHandler(ILogger<InitGrantedResourceCommandHandler> logger,IUnitOfWork unitOfWork, IGrantedResourceRepository repository)
    : ICommandHandler<InitGrantedResourceCommand>
{
    public async Task<Result> Handle(InitGrantedResourceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Databases",
                "GrantedResources.json");
            if (!File.Exists(path))
                return Result.Failure(GrantedResourceErrors.NotFound);

            var grantedResources = JsonConvert.DeserializeObject<GrantedResource[]>(
                await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken));
            if (grantedResources is null || grantedResources.Length == 0)
                return Result.Success();

            foreach (var resource in grantedResources)
            {
                var entity = await repository.GetByIdAsync(resource.Id);
                if (entity is null)
                    repository.Insert(resource);
                else
                {
                    entity.Update(resource.Role, resource.IdResource, resource.Actions, resource.Attributes);
                }
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