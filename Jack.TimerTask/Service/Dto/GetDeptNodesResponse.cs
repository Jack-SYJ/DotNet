using System;
using System.Collections.Generic;
using System.Text;

namespace Jack.TimerTask.Service
{
    public class GetDeptNodesResponse
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        public List<DepartmentInfo> data { get; set; }
    }

    /// <summary>
    /// LDAP部门信息
    /// </summary>
    public class DepartmentInfo
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 上级部门编码
        /// </summary>
        public string supDepCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string supComCode { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 用户列表
        /// </summary>
        public List<UserInfo> user { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public List<DepartmentInfo> children { get; set; }
    }

    public class UserInfo
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// LDAP用户Id
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 状态：在职、离职
        /// </summary>
        public string empStatus { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// 上级工号
        /// </summary>
        public string managerCode { get; set; }

        /// <summary>
        /// 上级名称
        /// </summary>
        public string managerName { get; set; }

        /// <summary>
        /// 上级Id
        /// </summary>
        public string managerUserId { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string depCode { get; set; }
    }
}
