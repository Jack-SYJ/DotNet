using System;
using System.Collections.Generic;
using Jack.Extend.MySQL;
using System.Threading;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;

namespace Jakc.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            IEnumerableTest();
            Console.ReadKey();
        }

        static void DataTableTest()
        {
            var datatable = new DataTable();
            datatable.TableName = "AppAuth";
            datatable.Columns.Add(new DataColumn
            {
                ColumnName = "accessId",
                DataType = typeof(Int32),
            });
            datatable.Columns.Add(new DataColumn
            {
                ColumnName = "appId",
                DataType = typeof(string),
                MaxLength = 50
            });
            datatable.Columns.Add(new DataColumn
            {
                ColumnName = "accessName",
                DataType = typeof(string),
                MaxLength = 200
            });
            datatable.Columns.Add(new DataColumn
            {
                ColumnName = "tenancyId",
                DataType = typeof(string),
                MaxLength = 50
            });
            datatable.Columns.Add(new DataColumn
            {
                ColumnName = "CreateTime",
                DataType = typeof(DateTime),
            });

            datatable.Columns.Add(new DataColumn
            {
                ColumnName = "UpdateTime",
                DataType = typeof(DateTime?).GetGenericArguments()[0],
            });

            for (int i = 0; i < 10; i++)
            {
                ArrayList array = new ArrayList();
                array.Add(0);
                array.Add($"id_{i}");
                array.Add($"name_{i}");
                array.Add($"name#@_''\"ten_{i}");
                array.Add(DateTime.Now);
                array.Add(null);
                datatable.LoadDataRow(array.ToArray(), true);
            }

            var batch = new MySqlBatcher();

            using (var connection = new MySqlConnection("server=9.134.119.192;userid=root;pwd=Tencent@2020;port=3306;database=test2;sslmode=none;"))
            {
                connection.Open();
                int count = 0;
                using (var command = connection.CreateCommand())
                {
                    batch.GenerateInserSql<MySqlParameter>(command, datatable);

                    if (command.CommandText == string.Empty)
                    {
                        return;
                    }

                    count = command.ExecuteNonQuery();
                }

                Console.WriteLine($" insert count [{count}]");
            }
        }
        static void IEnumerableTest()
        {
             DateTime dateTime = DateTime.Now;  
            for (int i = 0; i < 200; i++)
            {
                List<AppAuth> appAuths = new List<AppAuth>();

                for (int j = 0; j < 5000; j++)
                {
                    appAuths.Add(new AppAuth
                    {
                        appId = i*j + 154,
                        accessName = "accessName&*^%*^*&^%%*活动哈哦啊接哦我解耦我agoiaoigoiojoidjoiewj地奇偶决定我奥IjoiajoijoiJOIfjoiejoigjoidjaoij)(*",
                        tenancyId = "tencent''\"\"",
                        CreateTime = DateTime.Now,
                        UpdateTime = (j % 7 == 0) ? DateTime.Now : null
                    });
                }

                var batch = new MySqlBatcher();

                using (var connection = new MySqlConnection("server=127.0.0.1;userid=root;pwd=1000@TbTdTgT;port=3306;database=test2;sslmode=none;"))
                {
                    connection.Open();
                    int count = 0;
                    using (var command = connection.CreateCommand())
                    {
                        var result = batch.GenerateInserSql<AppAuth, MySqlParameter>(appAuths);

                        command.CommandText = result.Item1;
                        if (result.Item2.Count > 0)
                        {
                            command.Parameters.AddRange(result.Item2.ToArray());
                        }

                        count = command.ExecuteNonQuery();
                    }

                    Console.WriteLine($"{(DateTime.Now-dateTime).TotalSeconds}秒，循环值：[{i}] insert count [{count}]");
                }
            }
            
        }

    }
}
