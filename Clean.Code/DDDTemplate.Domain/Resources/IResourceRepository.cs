namespace DDDTemplate.Domain.Resources;

public interface IResourceRepository
{
    Task<Resource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Resource[]> GetByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);

    void Insert(Resource record);
    void Update(Resource record);
    void Remove(Resource record);
    void RemoveRange(IReadOnlyCollection<Resource> records);
    void InsertRange(IReadOnlyCollection<Resource> records);
    void UpdateRange(IReadOnlyCollection<Resource> records);
}