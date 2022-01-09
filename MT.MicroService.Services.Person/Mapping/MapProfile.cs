using AutoMapper;
using MT.MicroService.Core.Entity;
using MT.MicroService.Services.Person.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.MicroService.Services.Person.Mapping
{
    public class MapProfile : Profile
    {

        public MapProfile()
        {
            CreateMap<MT.MicroService.Core.Entity.Person, PersonDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
            CreateMap<MT.MicroService.Core.Entity.Person, PersonReadContactInfoDto>().ReverseMap();
        }
    }
}
