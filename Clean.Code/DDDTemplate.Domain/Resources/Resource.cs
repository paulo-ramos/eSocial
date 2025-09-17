using DDDTemplate.Domain.Abstractions.Data;
using DDDTemplate.Domain.GrantedResources;
using DDDTemplate.SharedKernel.Primitives;
using Newtonsoft.Json;

namespace DDDTemplate.Domain.Resources;

public class Resource : AggregateRoot, ISoftDeletableEntity, IAuditableEntity
{
#pragma warning disable
    private Resource()
    {
    }

    private Resource(Guid id, ResourceCode code, string name, string description, bool isBlocked) : base(id)
    {
        Id = id;
        Code = code;
        Name = name;
        Description = description;
        IsBlocked = isBlocked;
    }

    public ResourceCode Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsBlocked { get; set; }

    public bool IsDeleted { get; set; }

    [JsonIgnore] public Guid? DeletedBy { get; set; }
    [JsonIgnore] public DateTime? DeletedDateUtc { get; set; }
    [JsonIgnore] public Guid? CreatedBy { get; set; }
    [JsonIgnore] public DateTime CreatedDateUtc { get; set; }
    [JsonIgnore] public Guid? ModifiedBy { get; set; }
    [JsonIgnore] public DateTime? ModifiedDateUtc { get; set; }
    [JsonIgnore] public virtual ICollection<GrantedResource> GrantedResources { get; set; }


    public static Resource Create(Guid id, ResourceCode code, string name, string description, bool isBlocked)
    {
        return new Resource(id, code, name, description, isBlocked);
    }

    public Resource Update(ResourceCode code, string name, string description, bool isBlocked)
    {
        Code = code;
        Name = name;
        Description = description;
        IsBlocked = isBlocked;

        return this;
    }
}