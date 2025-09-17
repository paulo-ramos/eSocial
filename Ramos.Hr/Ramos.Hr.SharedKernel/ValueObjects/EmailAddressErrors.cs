using Ramos.Hr.SharedKernel.Primitives;

namespace Ramos.Hr.SharedKernel.ValueObjects;

public static class EmailAddressErrors
{
    public static readonly Error NullOrEmpty = new("Email.NullOrEmpty", "Email address is null or empty.");

    public static readonly Error InvalidFormat = new("Email.InvalidFormat", "Email address format is invalid.");
    
    public static readonly Error MaxLength = new("Email.MaxLength", $"Email address length must be less than or equal {EmailAddress.MaxLength:D0} characters.");
}