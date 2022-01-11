

namespace MT.ReportService.API.RabbitMQ
{
    public class CreateFileMessage
    {
        public int UserId { get; set; }

        public int FileId { get; set; }
    }

}