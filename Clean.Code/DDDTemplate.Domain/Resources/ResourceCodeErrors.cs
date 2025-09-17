using DDDTemplate.SharedKernel.Primitives;

namespace DDDTemplate.Domain.Resources;

public class ResourceCodeErrors
{
    public static readonly Error NullOrEmpty = new("ResourceCode.NullOrEmpty", "Organization code is null or empty.");

    public static readonly Error InvalidFormat =
        new("ResourceCode.InvalidFormat", "Organization code format is invalid.");

    public static readonly Error MaxLength = new("ResourceCode.MaxLength",
        $"Organization code length must be less than or equal {ResourceCode.MaxLength:D0} characters.");
}