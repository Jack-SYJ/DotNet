using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jack.TimerTask.Service
{
    public interface ILdapService
    {
        /// <summary>
        /// 根据部门Id获取组织结构及员工信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<GetDeptNodesResponse> GetDepartmentAllAsync(string deptId);
    }
}
