using DDDTemplate.SharedKernel.Primitives;

namespace DDDTemplate.Domain.Users;

public class UserErrors
{
    public static readonly Error NotFound = new(" User.NotFound", "The user is not found.");
    public static readonly Error ErrorInProcessing = new("User.ErrorInProcessing", "Error in processing.");

    public static readonly Error IncorrectUsernameOrPassword =
        new(" User.IncorrectUsernameOrPassword", "Incorrect username or password.");
    
    public static readonly Error AccountLockedByAdministrator =
        new(" User.AccountLockedByAdministrator", "Your account is locked by administrator.");
    
    public static readonly Error AccessDenied =
        new(" User.AccessDenied", "Access denied.");
}