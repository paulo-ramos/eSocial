using DDDTemplate.Domain.Employees;

namespace DDDTemplate.Application.Authentication.Common;

public record SigninInfoResultModel(SignedInUserModel User, string AccessToken, string RefreshToken);

public class SignedInUserModel(Employee employee)
{
    public Guid Id { get; set; } = employee.IdUser ?? Guid.Empty;
    public string FullName { get; set; } = employee.Fullname;
    public string Email { get; set; } = employee.EmailAddress.Value;
    public string Mobile { get; set; } = employee.MobileNumber.Value;
    public string AvatarUrl { get; set; } = employee.AvatarUrl;
}