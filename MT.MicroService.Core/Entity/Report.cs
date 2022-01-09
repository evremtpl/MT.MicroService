using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Core.Entity
{

    public enum FileStatus
    {
        Creating,
        Completed
    }
    public class Report
    {
        public int id { get; set; }
        public int UUID { get; set; }
      
        public DateTime RequestDate { get; set; }

        public FileStatus ReportState { get; set; }

    }
}
