using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.Resources;

namespace DDDTemplate.Domain.Utils;

public class CacheKeyUtil
{
    public static class GrantedResources
    {
        public static string GetByRoleAndResource(UserRoles role, ResourceCode resourceCode)
            => $"res_role:{resourceCode.Value},{role}";

        public static readonly string[] Patterns = ["res_role:*"];
    }
}