using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MT.MicroService.Services.Person.Dtos
{
    public class PersonDto
    {
        public int UUID { get; set; }
        [Required] //FluentValidation Kullanılabilir.
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        public string Company { get; set; }

    }
}
