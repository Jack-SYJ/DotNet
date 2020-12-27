using System;
using System.Collections.Generic;
using System.Text;

namespace Jack.TimerTask.JobScheduler
{
    public class JobConfig
    {

        /// <summary>
        /// Job总开关
        /// </summary>
        public bool Open { get; set; }

        /// <summary>
        /// 触发器集合
        /// </summary>
        public List<CronTrigger> CronTriggers { get; set; }
    }

    /// <summary>
    /// 触发器
    /// </summary>
    public class CronTrigger
    {
        /// <summary>
        /// Job 开关
        /// </summary>
        public bool Open { get; set; }

        /// <summary>
        /// Json名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        ///  //表达式（15分钟执行一次） 0 */15 * * * ?
        /// </summary>
        public string Expression { get; set; }

        public string JobType { get; set; }

        public string JobName { get; set; }

        public string JobGroup { get; set; }
    }
}
