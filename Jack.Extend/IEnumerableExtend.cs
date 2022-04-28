using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Collections;
using System.Reflection;

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
        /// <param name="defalueKeyValues">替换字段值为指定值，<字段名，指定值></param>
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

        /// <summary>
        ///  将集合 转成DataTable ,如集合没有值，返回null
        /// </summary>
        /// <typeparam name="T">必须是class</typeparam>
        /// <param name="value">待处理的value </param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> value) where T : class
        {
            if (!value.HasValue())
            {
                return null;
            }
            var properties = typeof(T).GetProperties();

            var dataTable = new DataTable();

            foreach (var item in properties)
            {
                dataTable.Columns.Add(new DataColumn(item.Name));
            }

            var length = dataTable.Columns.Count;
            for (int i = 0; i < value.Count(); i++)
            {
                var temp = new object[length];
                int j = 0;
                foreach (var propertyInfo in properties)
                {
                    object objValue = propertyInfo.GetValue(value.ElementAt(i), null);
                    temp[j] = objValue;

                    j++;
                }
                dataTable.LoadDataRow(temp, true);
            }
            return dataTable;
        }

        /// <summary>
        /// 将集合 转成DataTable ,如集合没有值，返回null
        /// </summary>
        /// <typeparam name="T">必须是class</typeparam>
        /// <typeparam name="AT">列名称特性</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T, AT>(this IEnumerable<T> value) where T : class where AT : System.ComponentModel.DescriptionAttribute
        {
            if (!value.HasValue())
            {
                return null;
            }
            var properties = typeof(T).GetProperties();
            var dataTable = new DataTable();
            List<string> invalidColumns = new List<string>(); //无效的列
            var attributeType = typeof(AT);
            var cloumnType = typeof(string);
            foreach (var item in properties)
            {
                var columnAttr = item.GetCustomAttributes(attributeType, true);
                if (columnAttr.Length > 0)
                {
                    dataTable.Columns.Add(new DataColumn(((AT)columnAttr[0]).Description, cloumnType));
                }
                else
                {
                    invalidColumns.Add(item.Name);
                }
            }

            var length = dataTable.Columns.Count;
            for (int i = 0; i < value.Count(); i++)
            {
                var temp = new object[length];
                int j = 0;
                foreach (var propertyInfo in properties)
                {
                    if (!invalidColumns.Contains(propertyInfo.Name))
                    {
                        object objValue = propertyInfo.GetValue(value.ElementAt(i), null);
                        temp[j] = objValue;
                        j++;
                    }
                }
                dataTable.LoadDataRow(temp, true);
            }
            return dataTable;
        }



        /// <summary>
        /// 将集合 转成DataTable ,如集合没有值，返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="headers">根据字段映射成指定列名称名称<字段名,列名></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> value, Dictionary<string, string> headers) where T : class
        {
            if (!value.HasValue() || !headers.HasValue())
            {
                return null;
            }
            var properties = typeof(T).GetProperties();
            var dataTable = new DataTable();
            foreach (var item in headers.Keys)
            {
                dataTable.Columns.Add(item);
            }

            var length = dataTable.Columns.Count;
            for (int i = 0; i < value.Count(); i++)
            {
                var tempArray = new object[length];
                int j = 0;
                foreach (var item in headers)
                {
                    var propertyInfo = properties.FirstOrDefault(x => x.Name.Equals(item.Value, StringComparison.OrdinalIgnoreCase));
                    if (propertyInfo == null)
                    {
                        tempArray[j] = null;
                    }
                    else
                    {
                        var objValue = propertyInfo.GetValue(value.ElementAt(i), null);
                        tempArray[j] = objValue;
                    }
                    j++;
                }

                dataTable.LoadDataRow(tempArray, true);
            }
            return dataTable;
        }

    }


}
