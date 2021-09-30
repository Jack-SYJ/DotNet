using Autofac;
using Autofac.Extras.Quartz;
using Jack.TimerTask.JobScheduler;
using Quartz.Logging;

namespace Jack.TimerTask.Jobs
{
    public static class ContainConfig
    {
        /// <summary>
        /// 注册 Job ，可以在job中指定
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static ContainerBuilder RegisterModule(this ContainerBuilder cb)
        {
            LogProvider.SetCurrentLogProvider(new JobLogProvider());
            // 1) Register IScheduler
            cb.RegisterModule(new QuartzAutofacFactoryModule());
            // 2) Register jobs
            cb.RegisterModule(new QuartzAutofacJobsModule(typeof(SyncJob).Assembly));
              
            return cb;
        }
    }
}
