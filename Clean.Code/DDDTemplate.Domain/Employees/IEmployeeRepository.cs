namespace DDDTemplate.Domain.Employees;

public interface IEmployeeRepository
{
    Task<bool> IsCodeUniqueAsync(string code, Guid? id = null, CancellationToken cancellationToken = default);
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Employee?> GetByIdUserAsync(Guid idUser, CancellationToken cancellationToken = default);

    Task<Employee[]> GetByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);

    void Insert(Employee record);
    void Remove(Employee record);
    void RemoveRange(IReadOnlyCollection<Employee> records);
    void InsertRange(IReadOnlyCollection<Employee> records);
    void UpdateRange(IReadOnlyCollection<Employee> records);

    IQueryable<Employee> GetQueryable(string keyword);
}