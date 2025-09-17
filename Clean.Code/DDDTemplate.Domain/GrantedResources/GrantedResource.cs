using DDDTemplate.Domain.Abstractions.Data;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.Resources;
using DDDTemplate.SharedKernel.Primitives;
using Newtonsoft.Json;

namespace DDDTemplate.Domain.GrantedResources;

public class GrantedResource : AggregateRoot, ISoftDeletableEntity, IAuditableEntity
{
#pragma warning disable
    private GrantedResource()
    {
    }

    private GrantedResource(Guid id, UserRoles role, Guid idResource, string[] actions, string[] attributes) : base(id)
    {
        Id = id;
        Role = role;
        IdResource = idResource;
        Actions = actions;
        Attributes = attributes;
    }

    public UserRoles Role { get; set; }
    public Guid IdResource { get; set; }
    public string[] Actions { get; set; }
    public string[] Attributes { get; set; }
    public bool IsDeleted { get; set; }
    [JsonIgnore] public Guid? DeletedBy { get; set; }
    [JsonIgnore] public DateTime? DeletedDateUtc { get; set; }
    [JsonIgnore] public Guid? CreatedBy { get; set; }
    [JsonIgnore] public DateTime CreatedDateUtc { get; set; }
    [JsonIgnore] public Guid? ModifiedBy { get; set; }
    [JsonIgnore] public DateTime? ModifiedDateUtc { get; set; }
    [JsonIgnore] public virtual Resource Resource { get; set; }

    public static GrantedResource Create(Guid id, UserRoles role, Guid idResource, string[] actions, string[] attributes)
    {
        return new GrantedResource(id, role, idResource, actions, attributes);
    }
    
    public  GrantedResource Update(UserRoles role, Guid idResource, string[] actions, string[] attributes)
    {
        Role = role;
        IdResource = idResource;
        Actions = actions;
        Attributes = attributes;

        return this;
    }
}