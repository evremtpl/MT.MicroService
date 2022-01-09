using System;
using System.Collections.Generic;

#nullable disable

namespace MT.MicroService.FileCreateWorkerService.Models
{
    public partial class Person
    {
        public int Uuid { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }
    }
}
