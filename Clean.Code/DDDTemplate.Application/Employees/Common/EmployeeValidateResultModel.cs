using DDDTemplate.Domain.Resources;
using DDDTemplate.Domain.UserPermissions;
using DDDTemplate.Domain.Users;

namespace DDDTemplate.Application.Employees.Common;

public record EmployeeValidateResultModel(Resource[] Resources, User? User, UserPermission[] UserPermissions);