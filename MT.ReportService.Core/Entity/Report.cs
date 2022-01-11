using System;


namespace MT.ReportService.Core.Entity
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


        public DateTime CreatedDate { get; set; }

        public FileStatus ReportState { get; set; }

    }
}
