using System.Security.Claims;
using DDDTemplate.Application.Abstractions.Services;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.GrantedResources;
using DDDTemplate.Domain.Resources;
using DDDTemplate.Domain.UserPermissions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace DDDTemplate.Infashtructure.Authentication;

public class ClaimsTransformation(
    IHttpContextAccessor httpContextAccessor,
    IIdentityService identityService,
    IGrantedResourceRepository grantedResourceRepository,
    IUserPermissionRepository userPermissionRepository
) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var endpointMetadata = httpContextAccessor.HttpContext?.GetEndpoint()?.Metadata;
        if (endpointMetadata is null)
            return principal;

        var resourceCode = endpointMetadata.GetMetadata<ResourceCode>();
        if (resourceCode is null) return principal;

        var role = await identityService.SetRoleAsync();
        if (role == UserRoles.None)
        {
            identityService.SetIsValid(false);
            return principal;
        }

        string[] actions;
        string[] attributes;
        var grantedResource = await grantedResourceRepository.GetByRoleAndResourceAsync(role, resourceCode);
        if (grantedResource == null)
        {
            identityService.SetIsValid(false);
            return principal;
        }

        if (role == UserRoles.Administrator)
        {
            actions = grantedResource.Actions;
            attributes = grantedResource.Attributes;
        }
        else
        {
            var permission =
                await userPermissionRepository.GetByIdUserAndIdResourceAsync((Guid)identityService.UserId!,
                    grantedResource.IdResource);

            if (permission == null)
            {
                identityService.SetIsValid(false);
                return principal;
            }

            actions = grantedResource.Actions;
            attributes = grantedResource.Attributes;
        }


        var claims = new List<Claim>();
        if (identityService.Role != UserRoles.None)
            claims.Add(new Claim("role", role.ToString()));

        claims.AddRange(actions.Select(x => new Claim("permissions", x)));
        principal.AddIdentity(new ClaimsIdentity(claims));

        identityService.SetResourceData(actions, attributes);
        return principal;
    }
}