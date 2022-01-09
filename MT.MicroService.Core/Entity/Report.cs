using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Core.Entity
{
    public class Report
    {

        public int UUID { get; set; }
      
        public DateTime RequestDate { get; set; }

        public int ReportState { get; set; }

    }
}
