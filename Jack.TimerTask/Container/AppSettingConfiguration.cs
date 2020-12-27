using System;
using System.Collections.Generic;
using System.Text;

namespace Jack.TimerTask.Container
{
    public class AppSettingConfiguration
    {
        public LdapService LdapService { get; set; }
    } 
    public class LdapService
    {
        public string Url { get; set; }
        public string Aes_Key { get; set; }
        public string Aes_Iv { get; set; }
        public string App_Secret { get; set; }
        public string Dept_Id { get; set; }  
    }
}
