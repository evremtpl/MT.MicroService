using MT.Microservis.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Core.Entity
{
    public class ContactInfo : EntityBase
    {
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string Location { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
