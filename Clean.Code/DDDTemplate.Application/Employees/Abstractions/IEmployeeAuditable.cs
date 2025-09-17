using DDDTemplate.Domain.Enums;

namespace DDDTemplate.Application.Employees.Abstractions;

public interface IEmployeeAuditable
{
    public string Code { get; set; }
    public string AvatarUrl { get; set; }
    public string Fullname { get; set; }
    public string EmailAddress { get; set; }
    public string MobileNumber { get; set; }
    public string Address { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsUserBlocked { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime? ExpirationDate { get; set; }

    public UserRoles? UserRole { get; set; }
    public Guid[] IdResources { get; set; }

    public Guid? IdUser { get; set; }
}