using DDDTemplate.SharedKernel.Primitives;

namespace DDDTemplate.Domain.GrantedResources;

public class GrantedResourceErrors
{
    public static readonly Error NotFound = new("GrantedResource.NotFound", "The granted resource is not found.");

    public static readonly Error ErrorInProcessing = new("GrantedResource.ErrorInProcessing", "Error in processing.");
}