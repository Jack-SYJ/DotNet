using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using MySql.Data.MySqlClient;

namespace Jack.Extend.MySQL
{
    /// <summary>
    /// 为 MySql.Data 组件提供的用于批量操作的方法。
    /// 参考 cnblogs.com/luluping/archive/2012/08/09/2629515.html
    /// </summary>
    public sealed class MySqlBatcher 
    {

        /// <summary>
        /// 将 <see cref="DataTable"/> 的数据批量插入到数据库中。
        /// </summary>
        /// <param name="dataTable">要批量插入的 <see cref="DataTable"/>。</param>
        public bool BulkInsert(DataTable dataTable,MySqlConnection connection,MySqlTransaction transaction=null)
        {
            if (dataTable.Rows.Count == 0)
            {
                return false;
            }
            if (connection == null)
            {
                return false;
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            } 
            using (var command = new MySqlCommand())
            {
                command.Connection = connection;
                command.CommandText = GenerateInserSql(command, dataTable);
                command.Transaction = transaction;
                if (command.CommandText == string.Empty)
                {
                    return false;
                }
                return command.ExecuteNonQuery()>0;
            }
        }

        /// <summary>
        /// 生成插入数据的sql语句。
        /// </summary>
        /// <param name="database"></param>
        /// <param name="command"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GenerateInserSql( DbCommand command, DataTable table)
        {
            var names = new StringBuilder();
            var values = new StringBuilder();
            var types = new List<DbType>();
            var count = table.Columns.Count;

            for (int j = 0; j < count; j++)
            {
                if (names.Length > 0)
                {
                    names.Append(",");
                }
                names.AppendFormat("{0}", table.Columns[j].ColumnName);
                types.Add((DbType)Enum.Parse(typeof(DbType), table.Columns[j].DataType.Name));
            }

            var i = 0;
            foreach (DataRow row in table.Rows)
            {
                if (i > 0)
                {
                    values.Append(",");
                }
                values.Append("(");
                for (var j = 0; j < count; j++)
                {
                    if (j > 0)
                    {
                        values.Append(", ");
                    }
                    var isStrType = IsStringType(types[j]);
                    var parameter = CreateParameter(isStrType, types[j], row[j], i, j);
                    if (parameter != null)
                    {
                        values.Append(parameter.ParameterName);
                        command.Parameters.Add(parameter);
                    }
                    else if (isStrType)
                    {
                        values.AppendFormat("'{0}'", row[j]);
                    }
                    else
                    {
                        values.Append(row[j]);
                    }
                }
                values.Append(")");
                i++;
            }
            return string.Format("INSERT INTO {0}({1}) VALUES {2}", table.TableName, names, values);
        }

        /// <summary>
        /// 判断是否为字符串类别。
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private bool IsStringType(DbType dbType)
        {
            return dbType == DbType.AnsiString || dbType == DbType.AnsiStringFixedLength || dbType == DbType.String || dbType == DbType.StringFixedLength;
        }

        /// <summary>
        /// 创建参数。
        /// </summary>
        /// <param name="isStrType"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private DbParameter CreateParameter(bool isStrType, DbType dbType, object value, int row, int col)
        {
            //如果生成全部的参数，则速度会很慢，因此，只有数据类型为字符串(包含'号)和日期型时才添加参数
            if ((isStrType && value.ToString().IndexOf('\'') != -1) || dbType == DbType.DateTime)
            {
                var name = string.Format("@p_{0}_{1}", row, col);
                var parameter = new MySqlParameter();
                parameter.ParameterName = name;
                parameter.Direction = ParameterDirection.Input;
                parameter.DbType = dbType;
                parameter.Value = value;
                return parameter;
            }
            return null;
        }
    }
}
