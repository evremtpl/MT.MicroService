using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.MicroService.Services.Person.Dtos
{
    public class ContactInfoDto
    {
       
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string Location { get; set; }
        public int UUID { get; set; }

    }
}
