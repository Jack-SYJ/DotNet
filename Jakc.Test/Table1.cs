using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jakc.Test
{
    [Table("AppAuth")]
    public class AppAuth
    {
        public int accessId { get; set; }

        public int appId { get; set; }

        public string accessName { get; set; }

        public string tenancyId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
