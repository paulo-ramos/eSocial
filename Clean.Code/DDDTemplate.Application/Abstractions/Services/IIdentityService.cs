using DDDTemplate.Domain.Abstractions.Data;
using DDDTemplate.Domain.Enums;

namespace DDDTemplate.Application.Abstractions.Services;

public interface IIdentityService
{
    public Guid? UserId { get; set; }
    public string? IdDevice { get; set; }
    public UserRoles Role { get; set; }
    public string[] ResourceActions { get; set; }
    public string[] ResourceAttribute { get; set; }
    public bool IsValid { get; set; }

    Task<UserRoles> SetRoleAsync();

    void SetResourceData(string[] resourceActions, string[] resourceAttributes);

    void SetIsValid(bool isValue = true);

    bool HasActions(params string[] actions);

    bool HasAllActions(params string[] actions);
    bool ValidRole<TEntity>(TEntity entity) where TEntity : IAuditableEntity;
    IQueryable<TEntity> ValidRole<TEntity>(IQueryable<TEntity> queryable) where TEntity : IAuditableEntity;
}