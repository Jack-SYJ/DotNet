using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Collections.Concurrent;
using System.Data.Common;

namespace Jack.Extend
{
    public static class IEnumerableExtend
    {

        /// <summary>
        /// 判断IEnumerable 是否有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasValue<T>(this IEnumerable<T> value)
        {
            if (value != null || value.Any())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  将集合 转成DataTable ,如集合没有值，返回null
        /// </summary>
        /// <typeparam name="T">必须是class</typeparam>
        /// <param name="value">待处理的value </param>
        /// <param name="defalueKeyValues">字段 需默认值</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> value, Dictionary<string, object> defalueKeyValues = null) where T : class
        {
            if (!value.HasValue())
            {
                return null;
            }
            var properties = typeof(T).GetProperties();
            var dataTable = new DataTable();
            dataTable.Columns.AddRange(properties.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());

            bool hasDefault = defalueKeyValues.HasValue();
            for (int i = 0; i < value.Count(); i++)
            {
                var tempList = new ArrayList();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    if (hasDefault && defalueKeyValues.ContainsKey(propertyInfo.Name)) // 赋默认值
                    {
                        tempList.Add(defalueKeyValues[propertyInfo.Name]);
                        continue;
                    }
                    object objValue = propertyInfo.GetValue(value.ElementAt(i), null);
                    tempList.Add(objValue);

                }
                var valueArray = tempList.ToArray();
                dataTable.LoadDataRow(valueArray, true);
            }
            return dataTable;
        }

       

    }
}
