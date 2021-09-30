using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jack.Extend
{
    class EntityProperties
    {
        /// <summary>
        /// 实体
        /// </summary>
        public Type Entity { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 字段信息 （字段名,字段反射信息,字段是否自增）
        /// </summary>
        public List<Tuple<string, PropertyInfo, bool>> Fields { get; set; }
    }
}
