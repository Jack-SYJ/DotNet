using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jakc.Test
{
    [Table("ddd")]
    public class Table1
    {
        public string id { get; set; }

        public DateTime date { get; set; }

        public DateTime? nullDate { get; set; }

        public int number { get; set; }

        public int? GetV { get; set; }
        public string id1 { get; set; }

        public DateTime date1 { get; set; }

        public DateTime? nullDate1 { get; set; }

        public int number1 { get; set; }

        public int? GetV1 { get; set; }

        public string id2 { get; set; }

        public DateTime date2 { get; set; }

        public DateTime? nullDate2 { get; set; }

        public int number2 { get; set; }

        public int? GetV2 { get; set; }

        public string id3 { get; set; }

        public DateTime date3 { get; set; }

        public DateTime? nullDate3 { get; set; }

        public decimal number3 { get; set; }

        public long? GetV3 { get; set; }
    }
}
