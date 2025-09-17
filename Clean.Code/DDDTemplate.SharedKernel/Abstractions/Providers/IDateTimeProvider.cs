namespace DDDTemplate.SharedKernel.Abstractions.Providers;

public interface IDateTimeProvider
{
    public DateTime Now { get; set; }
    public DateTime UtcNow { get; set; }
}