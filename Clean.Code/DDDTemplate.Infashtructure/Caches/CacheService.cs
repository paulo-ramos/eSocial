using DDDTemplate.SharedKernel.Abstractions.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DDDTemplate.Infashtructure.Caches;

public class CacheService : ICacheService
{
    private const string EmptyValue = "null";
    private readonly IConnectionMultiplexer _multiplexer;
    private readonly CachingSettings _settings;
    private readonly IDatabase _database;

    public CacheService(IConnectionMultiplexer multiplexer, IOptions<CachingSettings> options)
    {
        _multiplexer = multiplexer;
        _database = _multiplexer.GetDatabase();
        _settings = options.Value;
    }

    private string GetKey(string key) => $"{_settings.InstanceName}{key}";

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        string? value = await _database.StringGetAsync(GetKey(key));
        return value is null or EmptyValue ? default : JsonConvert.DeserializeObject<T>(value);
    }

    public async Task<string?> GetStringAsync(string key, CancellationToken cancellationToken = default)
    {
        string? value = await _database.StringGetAsync(GetKey(key));
        return value;
    }

    public Task SetStringAsync(string key, string value, CancellationToken cancellationToken = default)
        => SetStringAsync(key, value, null, cancellationToken);

    public Task SetStringAsync(string key, string value, DateTime? expiration,
        CancellationToken cancellationToken = default)
        => _database.StringSetAsync(GetKey(key), value, expiration?.Subtract(DateTime.Now));

    public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        => SetAsync(key, value, null, cancellationToken);

    public Task SetAsync<T>(string key, T value, DateTime? expiration, CancellationToken cancellationToken = default)
        => _database.StringSetAsync(GetKey(key),
            value is null || value.Equals(default(T)) ? EmptyValue : JsonConvert.SerializeObject(value),
            expiration?.Subtract(DateTime.Now));

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        => _database.KeyDeleteAsync(GetKey(key));

    public Task<string[]> GetKeys(string pattern = "*")
        => Task.FromResult(_multiplexer.GetServer(_multiplexer.GetEndPoints().First()).Keys(pattern: GetKey(pattern)).Select(x => x.ToString())
            .ToArray());

    public async Task<string[]> GetByKeysAsync(string pattern = "*", CancellationToken cancellationToken = default)
    {
        var keys = await GetKeys(pattern);
        var values = new List<string>();
        foreach (var key in keys)
        {
            string? str =  await _database.StringGetAsync(key);
            if (string.IsNullOrEmpty(str))
                continue;
            
            values.Add(str);
        }

        return values.ToArray();
    }

    public async Task RemoveByKeysAsync(string pattern = "*", CancellationToken cancellationToken = default)
    {
        var keys = await GetKeys(pattern);
        foreach (var key in keys)
            await _database.KeyDeleteAsync(key);
    }

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, DateTime? expiration, CancellationToken cancellationToken = default)
    {
        string? cacheValue = await _database.StringGetAsync(GetKey(key));
        if (cacheValue is not null)
            return cacheValue is EmptyValue ? default : JsonConvert.DeserializeObject<T>(cacheValue);
            
        var data = await factory().ConfigureAwait(false);
        await SetAsync(key, data, expiration, cancellationToken);

        return data;
    }

    public Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default)
        => GetOrCreateAsync(key, factory, null, cancellationToken);

    public Task<long> IncrAsync(string key)
        => _database.StringIncrementAsync(GetKey(key));
}