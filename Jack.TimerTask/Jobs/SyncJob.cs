using Castle.Components.DictionaryAdapter;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Jack.TimerTask.Container;
using Jack.TimerTask.Service;

namespace Jack.TimerTask.Jobs
{
    /// <summary>
    /// 从LDAP 同步组织及员工
    /// </summary>
    [DisallowConcurrentExecution]
    public class SyncJob : IJob
    {
        private IOptionsSnapshot<AppSettingConfiguration> _options;
      


        public SyncJob(IOptionsSnapshot<AppSettingConfiguration> options)
        {
            this._options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public  Task Execute(IJobExecutionContext context)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
               // throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
