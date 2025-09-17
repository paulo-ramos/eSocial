namespace DDDTemplate.Domain.Abstractions.Data;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
    Guid? DeletedBy { get; set; }
    DateTime? DeletedDateUtc { get; set; }
}