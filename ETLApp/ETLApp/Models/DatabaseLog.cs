using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLApp.Models
{
    [Table("tbLog")]
    public class DatabaseLog
    {
        public int LogID { get; set; }  
        public string? RunStatus { get; set; }
        public DateTime RunTime { get; set; }
    }
}
