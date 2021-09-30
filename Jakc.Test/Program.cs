using System;
using System.Collections.Generic;
using Jack.Extend.MySQL;
using System.Threading;
namespace Jakc.Test
{
    class Program
    {
        static void Main(string[] args)
        {

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

            var result = table1s.CreateBulkSql();

            Console.WriteLine("Hello World!");
        }
    }
}
