namespace DDDTemplate.Domain.Abstractions.Data;

public interface IAuditableEntity
{
    Guid? CreatedBy { get; set; }
    DateTime CreatedDateUtc { get; set; }
    Guid? ModifiedBy { get; set; }
    DateTime? ModifiedDateUtc { get; set; }
}