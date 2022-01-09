
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Core.Entity
{
   public class Person 
    {
        public Person()
        {
            ContactInfos = new Collection<ContactInfo>();
        }
        
        public int UUID { get; set; }
        public string Name { get; set; }

        public string SurName { get; set; }

        public string Company { get; set; }

        public ICollection<ContactInfo> ContactInfos { get; set; }
    }
}
