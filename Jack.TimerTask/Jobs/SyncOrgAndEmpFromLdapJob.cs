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
    public class SyncOrgAndEmpFromLdapJob : IJob
    {
        private readonly ILdapService _ldapService;
        private IOptionsSnapshot<AppSettingConfiguration> _options;
      


        public SyncOrgAndEmpFromLdapJob(IOptionsSnapshot<AppSettingConfiguration> options, ILdapService ldapService)
        {
            this._ldapService = ldapService;
            this._options = options;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
