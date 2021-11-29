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

            for (int i = 0; i < 10; i++)
            {
                ArrayList array = new ArrayList();
                array.Add(0);
                array.Add($"id_{i}");
                array.Add($"name_{i}");
                array.Add($"name#@_''\"ten_{i}");
                datatable.LoadDataRow(array.ToArray(), true);
            }

            List<Table1> table1s = new List<Table1>();
            for (int i = 0; i < 3; i++)
            {
                table1s.Add(new Table1
                {
                    id = i.ToString(),
                    date = DateTime.Now,
                    nullDate = null,
                    GetV = i + 100
                });
                Thread.Sleep(1000);
            }
            var batch = new MySqlBatcher();

            // var table = new DataTable();

            using (var connection = new MySqlConnection("server=9.134.119.192;userid=root;pwd=Tencent@2020;port=3306;database=test2;sslmode=none;"))
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    if (command == null)
                    {
                        throw new ArgumentException("command");
                    }
                    command.Connection = connection;


                    command.Transaction = null;


                    var para = new MySqlParameter();
                   // batch.GenerateInserSql(datatable, command,para );


                    if (command.CommandText == string.Empty)
                    {
                        return;
                    }


                    command.ExecuteNonQuery();
                }




                //var result = table1s.CreateBulkSql();

                Console.WriteLine("Hello World!");
            }
        }
    }
}
