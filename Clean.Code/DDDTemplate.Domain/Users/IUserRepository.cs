namespace DDDTemplate.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> IsUsernameUniqueAsync(string username, Guid? id = null, CancellationToken cancellationToken = default);

    Task<User[]> GetByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);

    void Insert(User record);
    void Remove(User record);
    void RemoveRange(IReadOnlyCollection<User> records);
    void InsertRange(IReadOnlyCollection<User> records);
    void UpdateRange(IReadOnlyCollection<User> records);
}