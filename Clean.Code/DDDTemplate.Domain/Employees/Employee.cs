using DDDTemplate.Domain.Abstractions.Data;
using DDDTemplate.Domain.UserLogs;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.Primitives;
using DDDTemplate.SharedKernel.ValueObjects;
using Newtonsoft.Json;

namespace DDDTemplate.Domain.Employees;

public class Employee : AggregateRoot, ISoftDeletableEntity, IAuditableEntity
{
#pragma warning disable
    private Employee()
    {
    }

    private Employee(Guid id, string code, string avatarUrl, string fullname,
        EmailAddress emailAddress, MobileNumber mobileNumber, string address, bool isBlocked, Guid? idUser) : base(id)
    {
        Id = id;
        Code = code;
        AvatarUrl = avatarUrl;
        Fullname = fullname;
        EmailAddress = emailAddress;
        MobileNumber = mobileNumber;
        Address = address;
        IsBlocked = isBlocked;
        IdUser = idUser;
    }

    public string Code { get; set; }
    public string AvatarUrl { get; set; }
    public string Fullname { get; set; }
    public EmailAddress EmailAddress { get; set; }
    public MobileNumber MobileNumber { get; set; }
    public string Address { get; set; }
    public bool IsBlocked { get; set; }

    public bool IsDeleted { get; set; }
    public Guid? IdUser { get; set; }

    [JsonIgnore] public Guid? DeletedBy { get; set; }
    [JsonIgnore] public DateTime? DeletedDateUtc { get; set; }
    [JsonIgnore] public Guid? CreatedBy { get; set; }
    [JsonIgnore] public DateTime CreatedDateUtc { get; set; }
    [JsonIgnore] public Guid? ModifiedBy { get; set; }
    [JsonIgnore] public DateTime? ModifiedDateUtc { get; set; }
    [JsonIgnore] public virtual User? User { get; set; }


    public void Update(string code, string avatarUrl, string fullname,
        EmailAddress emailAddress, MobileNumber mobileNumber, string address, bool isBlocked, Guid? idUser)
    {
        Code = code;
        AvatarUrl = avatarUrl;
        Fullname = fullname;
        EmailAddress = emailAddress;
        MobileNumber = mobileNumber;
        Address = address;
        IsBlocked = isBlocked;
        IdUser = idUser;
        Raise(new PushUserLogDomainEvent(nameof(Employee), nameof(Update),
            "Update employee {1} (ID: {0}, code: {3}).",
            this, [Id, Fullname, Code]));
    }

    public void Delete()
    {
        Raise(new PushUserLogDomainEvent(nameof(Employee), nameof(Delete),
            "Delete employee {1} (ID: {0}, code: {3}).",
            this, [Id, Fullname, Code]));
    }

    public static Employee Create(string code, string avatarUrl, string fullname,
        EmailAddress emailAddress, MobileNumber mobileNumber, string address, bool isBlocked, Guid? idUser)
    {
        var record = new Employee(Guid.NewGuid(), code, avatarUrl, fullname, emailAddress,
            mobileNumber,
            address, isBlocked, idUser);

        record.Raise(new PushUserLogDomainEvent(nameof(Employee), nameof(Create),
            "Create employee {1} (ID: {0}, code: {3}).",
            record, [record.Id, record.Fullname, record.Code]));

        return record;
    }
}