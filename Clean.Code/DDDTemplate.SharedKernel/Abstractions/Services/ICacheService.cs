namespace DDDTemplate.SharedKernel.Abstractions.Services;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    Task<string?> GetStringAsync(string key, CancellationToken cancellationToken = default);

    Task SetStringAsync(string key, string value, CancellationToken cancellationToken = default);

    Task SetStringAsync(string key, string value, DateTime? expiration, CancellationToken cancellationToken = default);

    Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default);

    Task SetAsync<T>(string key, T value, DateTime? expiration, CancellationToken cancellationToken = default);

    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    Task<string[]> GetKeys(string pattern = "*");

    Task<string[]> GetByKeysAsync(string pattern = "*", CancellationToken cancellationToken = default);

    Task RemoveByKeysAsync(string pattern = "*", CancellationToken cancellationToken = default);

    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, DateTime? expiration,
        CancellationToken cancellationToken = default);

    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory,
        CancellationToken cancellationToken = default);

    Task<long> IncrAsync(string key);
}