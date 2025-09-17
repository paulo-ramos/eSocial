namespace DDDTemplate.WebApi.Enums;

public class EndpointRoutes
{
    public class Authentication
    {
        public const string Signin = "/authentication/signin";
        public const string RenewAccessToken = "/authentication/renew-accesstoken";
    }
    public class Test
    {
        public const string Get = "/test/get";
    }

    public class Employee
    {
        public const string Create = "/employee/create";
        public const string Update = "/employee/update";
        public const string Delete = "/employee/delete";
        public const string Get = "/employee/get/{id}";
        public const string List = "/employee/list";
    }
}