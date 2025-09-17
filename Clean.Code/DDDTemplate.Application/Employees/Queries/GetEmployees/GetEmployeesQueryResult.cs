namespace DDDTemplate.Application.Employees.Queries.GetEmployees;

public class GetEmployeesQueryResult
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
}