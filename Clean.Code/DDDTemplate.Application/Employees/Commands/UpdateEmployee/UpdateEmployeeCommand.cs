using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Employees.Abstractions;
using DDDTemplate.Domain.Enums;

namespace DDDTemplate.Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IEmployeeAuditable, ICommand
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public bool IsUserBlocked { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public DateTime? ExpirationDate { get; set; }
    public UserRoles? UserRole { get; set; }
    public Guid[] IdResources { get; set; } = [];
    public Guid? IdUser { get; set; }
}