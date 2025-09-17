using Microsoft.Extensions.Options;
using Quartz;

namespace DDDTemplate.Infashtructure.Jobs;

public class BackgroundJobsSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var keyInitDataJob = JobKey.Create(nameof(InitDataJob));
        options.AddJob<InitDataJob>(builder => builder.WithIdentity(keyInitDataJob))
            .AddTrigger(trigger => trigger.ForJob(keyInitDataJob).StartNow());
    }
}