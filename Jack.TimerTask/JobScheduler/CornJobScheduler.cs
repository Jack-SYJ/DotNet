using Jack.TimerTask.JobScheduler;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jack.TimerTask
{
    public sealed class CornJobScheduler : ICornJobScheduler, IDisposable
    {
        private readonly IScheduler _scheduler;
        private readonly JobConfig _jobConfig;
        private readonly ILogger<CornJobScheduler> _logger;
        public CornJobScheduler(IScheduler scheduler, IOptionsMonitor<JobConfig> jobConfig, ILogger<CornJobScheduler> logger)
        {
            _scheduler = scheduler;
            _jobConfig = jobConfig.CurrentValue;
            _logger = logger;
        }
        public async void Dispose()
        {
            if (_scheduler != null)
            {
                await _scheduler.Shutdown(true);
            }
        }
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("code:debug");
            if (_jobConfig.Open && _jobConfig.CronTriggers.Any())
            {

                foreach (var trigger in _jobConfig.CronTriggers)
                {
                    if (trigger.Open)
                    {

                        var jobType = Type.GetType(trigger.JobType);
                        var jobDataMap = new JobDataMap();

                        var job = JobBuilder.Create(jobType)
                            .WithIdentity(trigger.JobName, trigger.JobGroup)
                            .SetJobData(jobDataMap).Build();    //创建一个任务                                
                        var cronTrigger = TriggerBuilder.Create()
                            .WithIdentity(trigger.Name, trigger.Group)
                            .StartNow()
                            .WithCronSchedule(trigger.Expression)
                            .Build();

                        await _scheduler.ScheduleJob(job, cronTrigger, cancellationToken);

                    }
                }
                await _scheduler.Start();

            }
        }
    }
}
