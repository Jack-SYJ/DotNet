using System;
using System.Collections.Generic;
using System.Text;

namespace Jack.TimerTask.Service.Dto
{
    public class GetDeptNodesRequest
    {
        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        public string deptId { get; set; }
    }
}
