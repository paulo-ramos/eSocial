using DDDTemplate.SharedKernel.Primitives;

namespace DDDTemplate.SharedKernel.ValueObjects;

public static class MobileNumberErrors
{
    public static readonly Error NullOrEmpty = new("MobileNumber.NullOrEmpty", "Mobile number is null or empty.");

    public static readonly Error InvalidFormat = new("MobileNumber.InvalidFormat", "Mobile number format is invalid.");
    
    public static readonly Error MaxLength = new("MobileNumber.MaxLength", $"Mobile number length must be less than or equal {MobileNumber.MaxLength:D0} characters.");
}