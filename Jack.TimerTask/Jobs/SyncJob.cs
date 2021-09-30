using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Jack.TimerTask.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    [DisallowConcurrentExecution]
    public class SyncJob : IJob
    {
        public SyncJob()
        {
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
