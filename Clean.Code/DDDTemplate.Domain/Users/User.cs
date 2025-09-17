using DDDTemplate.Domain.Abstractions.Data;
using DDDTemplate.Domain.Employees;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.UserLogs;
using DDDTemplate.SharedKernel.Primitives;
using Newtonsoft.Json;

namespace DDDTemplate.Domain.Users;

public class User : AggregateRoot, ISoftDeletableEntity, IAuditableEntity
{
#pragma warning disable
    private User()
    {
    }

    private User(Guid id, string username, Password password, bool isBlocked, DateTime? expirationDate,
        UserRoles userRole) : base(id)
    {
        Username = username;
        Password = password;
        IsBlocked = isBlocked;
        ExpirationDate = expirationDate;
        UserRole = userRole;
    }

    public string Username { get; set; }
    public Password Password { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public UserRoles UserRole { get; set; }
    public bool IsBlocked { get; set; }

    public bool IsDeleted { get; set; }

    [JsonIgnore] public Guid? DeletedBy { get; set; }
    [JsonIgnore] public DateTime? DeletedDateUtc { get; set; }
    [JsonIgnore] public Guid? CreatedBy { get; set; }
    [JsonIgnore] public DateTime CreatedDateUtc { get; set; }
    [JsonIgnore] public Guid? ModifiedBy { get; set; }
    [JsonIgnore] public DateTime? ModifiedDateUtc { get; set; }

    [JsonIgnore] public virtual Employee Employee { get; set; }

    public void Update(string username, Password password, bool isBlocked, DateTime? expirationDate, UserRoles userRole)
    {
        Username = username;
        Password = password;
        IsBlocked = isBlocked;
        ExpirationDate = expirationDate;
        UserRole = userRole;

        Raise(new PushUserLogDomainEvent(nameof(User), nameof(Update),
            "Update user {1} (ID: {0}).",
            this, [Id, Username]));
    }

    public void Delete()
    {
        Raise(new PushUserLogDomainEvent(nameof(User), nameof(Delete),
            "Delete user {1} (ID: {0}).",
            this, [Id, Username]));
    }

    public static User Create(Guid? id, string username, Password password, bool isBlocked, DateTime? expirationDate,
        UserRoles userRole)
    {
        var record = new User(id ?? Guid.NewGuid(), username, password, isBlocked, expirationDate, userRole);
        record.Raise(new PushUserLogDomainEvent(nameof(User), nameof(Create),
            "Update user {1} (ID: {0}).",
            record, [record.Id, record.Username]));

        return record;
    }
}