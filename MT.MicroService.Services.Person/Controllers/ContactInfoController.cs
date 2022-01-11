using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.MicroService.Core.Entity;
using MT.MicroService.Core.Services;
using MT.MicroService.Services.Person.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.MicroService.Services.Person.Controllers
{
    [Route("api/[controller]/[action]")] //best practise açısından method conf a göre gitmek daha uygun olurdu.
    [ApiController]
    public class ContactInfoController : ControllerBase
    {

        private readonly IContactInfoService _contactInfoService;
        private readonly IMapper _mapper;

        public ContactInfoController(IContactInfoService contactInfoService, IMapper mapper)
        {
            _contactInfoService = contactInfoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddContactInfotoPerson(ContactInfoDto contactInfoDto)
        {
            var newContactInfo = await _contactInfoService.AddAsync(_mapper.Map<MT.MicroService.Core.Entity.ContactInfo>(contactInfoDto));
            return Created(string.Empty, _mapper.Map<ContactInfoDto>(newContactInfo));
            

        }

        [HttpDelete]
        public IActionResult RemoveContactInfoFromPerson(int id)
        {
            var contactInfo = _contactInfoService.GetByIdAsync(id).Result;
            if (contactInfo != null)
            {
                _contactInfoService.Delete(contactInfo);

                return NoContent();
            }
            return BadRequest("Silinecek kayıt yok");
        }
    }
}
