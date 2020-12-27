using Autofac;
using Autofac.Extras.Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using Jack.TimerTask.Service;

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
            // 1) Register IScheduler
            cb.RegisterModule(new QuartzAutofacFactoryModule());
            // 2) Register jobs
            cb.RegisterModule(new QuartzAutofacJobsModule(typeof(SyncOrgAndEmpFromLdapJob).Assembly));
              
            return cb;
        }
    }
}
