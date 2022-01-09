using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.MicroService.Services.Person.RabbitMQ
{
    public class CreateFileMessage
    {
        public int UserId { get; set; }

        public int FileId { get; set; }
    }

}