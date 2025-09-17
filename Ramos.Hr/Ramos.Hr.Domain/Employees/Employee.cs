using Ramos.Hr.SharedKernel.ValueObjects;

namespace Ramos.Hr.Domain.Employees;

public class Employee
{
    private Employee()
    {
        
    }
    
    public Employee(string code, string fullname, EmailAddress emailAddress)
    {
        Code = code;
        Fullname = fullname;
        EmailAddress = emailAddress;
    }
    
    public string Code { get; set; }
    public string Fullname { get; set; }
    public EmailAddress EmailAddress { get; set; }
    
    
    
    public void Update(string code, string fullname, EmailAddress emailAddress)
    {
        Code = code;
        Fullname = fullname;
        EmailAddress = emailAddress;
    }
}