using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.ReportService.API.Dtos
{
    public enum FileStatus
    {
        Creating,
        Completed
    }
    public class ReportDto
    {
        public int UUID { get; set; }

        public DateTime RequestDate { get; set; }


        public FileStatus ReportState { get; set; }
    }
}
