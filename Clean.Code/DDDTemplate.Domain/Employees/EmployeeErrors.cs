using DDDTemplate.SharedKernel.Primitives;

namespace DDDTemplate.Domain.Employees;

public class EmployeeErrors
{
    public static readonly Error NotFound = new(" Employee.NotFound", "The employee is not found.");
    public static readonly Error Unauthorized = new(" Employee.Unauthorized", "Unauthorized.");
    public static readonly Error ErrorInProcessing = new("Employee.ErrorInProcessing", "Error in processing.");

    public static readonly Error DuplicateUsername =
        new("Employee.DuplicateUsername", "The specified username is already in use.");

    public static readonly Error UserRoleWrongFormat =
        new("Employee.UserRoleWrongFormat", "The user role is wrong format.");

    public static readonly Error ResourcesIsRequired =
        new("Employee.ResourcesIsRequired", "The resources are required.");

    public static readonly Error ResourcesNotFound = new(" Employee.ResourcesNotFound", "The resources is not found.");

    public static readonly Error UserInfoIsRequired = new(" Employee.UserInfoIsRequired", "The user info is required.");
    public static readonly Error UserIsNotFound = new(" Employee.UserIsNotFound", "The user is not found.");
    public static readonly Error DuplicateCode =
        new("Employee.DuplicateCode", "The specified code is already in use.");
}