using Microsoft.Extensions.Logging;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jack.TimerTask.JobScheduler
{
    /// <summary>
    /// 日志监听器
    /// </summary>
    public class JobLogProvider : ILogProvider
    {
       // ILogger<JobLogProvider> _logger;
        //public JobLogProvider(ILogger<JobLogProvider> logger)
        //{
        //    _logger = logger;
        
        //}
        public Logger GetLogger(string name)
        {
            return new Logger((level, func, exception, parameters) =>
            {
                if (level >= Quartz.Logging.LogLevel.Error && func != null)
                {
                    // 记录文本日志
                    //_logger.LogError("JobException", exception);
                }
                return true;
            });
        }

        public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

    }
}
