using DDDTemplate.Domain.GrantedResources;
using DDDTemplate.Domain.Resources;
using DDDTemplate.Domain.Users;
using Newtonsoft.Json;

namespace DDDTemplate.Domain.UserPermissions;

public class UserPermission
{
#pragma warning disable
    private UserPermission()
    {
    }

    public UserPermission(Guid idUser, Guid idGrantedResource, Guid idResource)
    {
        IdUser = idUser;
        IdGrantedResource = idGrantedResource;
        IdResource = idResource;
    }

    public Guid IdUser { get; set; }
    public Guid IdResource { get; set; }
    public Guid IdGrantedResource { get; set; }
    [JsonIgnore] public User User { get; set; }
    [JsonIgnore] public Resource Resource { get; set; }
    [JsonIgnore] public GrantedResource GrantedResource { get; set; }
}