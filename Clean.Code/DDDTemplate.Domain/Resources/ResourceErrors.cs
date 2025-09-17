using DDDTemplate.SharedKernel.Primitives;

namespace DDDTemplate.Domain.Resources;

public class ResourceErrors
{
    public static readonly Error DuplicateCode =
        new("Resource.DuplicateCode", "The specified code is already in use.");

    public static readonly Error NotFound = new("Resource.NotFound", "The resource is not found.");
    
    public static readonly Error ErrorInProcessing = new("Resource.ErrorInProcessing", "Error in processing.");
}