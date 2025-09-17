using DDDTemplate.SharedKernel.Abstractions.Providers;

namespace DDDTemplate.Infashtructure.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now { get; set; } = DateTime.Now;
    public DateTime UtcNow { get; set; } = DateTime.UtcNow;
}