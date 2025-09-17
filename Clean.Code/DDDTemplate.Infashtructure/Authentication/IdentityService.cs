using DDDTemplate.Application.Abstractions.Services;
using DDDTemplate.Domain.Abstractions.Data;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.Enums;
using Microsoft.AspNetCore.Http;

namespace DDDTemplate.Infashtructure.Authentication;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public IdentityService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;

        SetUserId();
        IdDevice = GetHeaderValue(HttpHeaders.XDeviceId);
    }

    public Guid? UserId { get; set; }
    public string? IdDevice { get; set; }
    public UserRoles Role { get; set; }
    public string[] ResourceActions { get; set; } = [];
    public string[] ResourceAttribute { get; set; } = [];
    public bool IsValid { get; set; }

    public async Task<UserRoles> SetRoleAsync()
    {
        if (UserId == null)
        {
            Role = UserRoles.None;
            return Role;
        }

        var user = await _userRepository.GetByIdAsync((Guid)UserId);
        Role = user?.UserRole ?? UserRoles.None;

        return Role;
    }

    public void SetResourceData(string[] resourceActions, string[] resourceAttributes)
    {
        ResourceActions = resourceActions;
        ResourceAttribute = resourceAttributes;
    }

    public void SetIsValid(bool isValue = true)
    {
        IsValid = isValue;
    }

    public bool HasActions(params string[] actions)
    {
        return actions.Length == 0 || actions.Any(ResourceActions.Contains);
    }

    public bool HasAllActions(params string[] actions)
    {
        return actions.Length == 0 || actions.All(ResourceActions.Contains);
    }

    public bool ValidRole<TEntity>(TEntity entity) where TEntity : IAuditableEntity
    {
        if (UserId == null)
            return false;

        return Role switch
        {
            UserRoles.None => false,
            UserRoles.Administrator or UserRoles.Manager => true,
            _ => entity.CreatedBy == UserId
        };
    }

    public IQueryable<TEntity> ValidRole<TEntity>(IQueryable<TEntity> queryable) where TEntity : IAuditableEntity
    {
        if (UserId == null)
            return Enumerable.Empty<TEntity>().AsQueryable();
        
        return Role switch
        {
            UserRoles.None =>  Enumerable.Empty<TEntity>().AsQueryable(),
            UserRoles.Administrator or UserRoles.Manager => queryable,
            _ => queryable.Where(x => x.CreatedBy == UserId)
        };
    }

    private string? GetHeaderValue(string name)
    {
        return _httpContextAccessor.HttpContext?.Request.Headers[name];
    }

    private void SetUserId()
    {
        var value = GetHeaderValue(HttpHeaders.XUserId);
        UserId = Guid.TryParse(value, out var parseId) ? parseId : null;
    }
}