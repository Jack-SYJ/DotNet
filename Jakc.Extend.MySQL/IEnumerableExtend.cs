using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jack.Extend.MySQL
{
    public static class IEnumerableExtend
    {

        private static ConcurrentDictionary<Type, EntityProperties> CacheTypeDic = new ConcurrentDictionary<Type, EntityProperties>();



        /// <summary>
        ///返回拼接好的sql，无参
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        private static string CreateSql<T>(IEnumerable<T> values) where T : class
        {
            var entityProperties = GetEntity(values.First().GetType());
            //var fieldBuilder = new StringBuilder();
            var valueBuilder = new StringBuilder();
            var index = 0;
            var count = values.Count();
            foreach (var item in values)
            {
                index++;
                valueBuilder.Append("(");
                var isAppend = false;
                foreach (var (name, propertiy, isPrimary) in entityProperties.Fields)
                {
                    var value = propertiy.GetValue(item);
                    if (isAppend)
                    {
                        valueBuilder.Append(",");
                    }
                    if (value == null)
                    {
                        valueBuilder.Append("NULL");
                    }
                    else
                    {
                        if (value is string)
                        {
                            var strV = value.ToString();
                            strV.Replace("\"", " "); //将双引号改为空格
                            valueBuilder.Append($"\"{strV}\"");
                        }
                        else
                        {
                            valueBuilder.Append($"\"{value}\"");
                        }
                    }
                    isAppend = true;
                }
                if (index != count)
                {
                    valueBuilder.Append("),");
                }
                else
                {
                    valueBuilder.Append(")");
                }
            }
            return $"INSERT INTO {entityProperties.TableName} ({string.Join(",", entityProperties.Fields.Select(x => x.Item1))}) VALUES {valueBuilder.ToString()};";
        }


        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        private static string CreateParameterSql<T, P>(IEnumerable<T> values, List<P> dbParameters) where T : class where P : DbParameter, new()
        {
          
            var entityProperties = GetEntity(values.First().GetType());
            var valueBuilder = new StringBuilder();
            var index = 0;
            var count = values.Count();
            foreach (var item in values)
            {
                index++;
                valueBuilder.Append("(");
                var isAppend = false;
                foreach (var (name, propertiy, isPrimary) in entityProperties.Fields)
                {
                    var value = propertiy.GetValue(item);

                    //dbParameters.Add(new P("", ""));

                    if (isAppend)
                    {
                        valueBuilder.Append(",");
                    }
                    if (value == null)
                    {
                        valueBuilder.Append("NULL");
                    }
                    else
                    {
                        if (value is string)
                        {
                            var strV = value.ToString();
                            strV.Replace("\"", " "); //将双引号改为空格
                            valueBuilder.Append($"\"{strV}\"");
                        }
                        else
                        {
                            valueBuilder.Append($"\"{value}\"");
                        }
                    }
                    isAppend = true;
                }
                if (index != count)
                {
                    valueBuilder.Append("),");
                }
                else
                {
                    valueBuilder.Append(")");
                }
            }
            return $"INSERT INTO {entityProperties.TableName} ({string.Join(",", entityProperties.Fields.Select(x => x.Item1))}) VALUES {valueBuilder.ToString()};";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static EntityProperties GetEntity(Type entity)
        {
            if (!CacheTypeDic.ContainsKey(entity))
            {
                var entityProperties = new EntityProperties();
                entityProperties.Entity = entity;
                entityProperties.TableName = GetCustomName(entity);
                entityProperties.Fields = new List<Tuple<string, PropertyInfo, bool>>();
                //获取字段
                foreach (var propertiy in entity.GetProperties())
                {
                    entityProperties.Fields.Add(new Tuple<string, PropertyInfo, bool>(GetCustomName(propertiy), propertiy, false));
                }
                CacheTypeDic.TryAdd(entity, entityProperties);
                return entityProperties;
            }
            return CacheTypeDic[entity];

        }

        /// <summary>
        /// 获取自定义表名称
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private static string GetCustomName(Type entityType)
        {
            object nameType = entityType.GetCustomAttributes(true)
                .FirstOrDefault(x => x.GetType().Equals(typeof(TableAttribute)));

            if (nameType != null)
            {
                var tableName = (nameType as TableAttribute).Name;
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    return entityType.Name;
                }
                return tableName;
            }
            else
            {
                return entityType.Name;
            }
        }

        /// <summary>
        /// 获取自定义字段名称
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private static string GetCustomName(MemberInfo entityType)
        {
            var nameType = entityType.GetCustomAttributes(true)
             .FirstOrDefault(x => x.GetType().Equals(typeof(ColumnAttribute)));

            if (nameType != null)
            {
                var tableName = (nameType as ColumnAttribute).Name;
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    return entityType.Name;
                }
                return tableName;
            }
            else
            {
                return entityType.Name;
            }
        }


        /// <summary>
        /// 获取实体名称或属性名称
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private static string GetName(Type entityType)
        {
            return entityType.Name;
        }


    }
}
