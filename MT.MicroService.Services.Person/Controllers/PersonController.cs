using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.MicroService.Core.Services;
using MT.MicroService.Services.Person.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.MicroService.Services.Person.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> KisileriListele()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(persons));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> KisiyiGetir(int id) //db de Id var mı yok mu diye bir kontrol yapılmadı! Bir filter yazılabilir kod tekrarı olmaması için
        {
            var person = await _personService.GetByIdAsync(id);
            return Ok(_mapper.Map<PersonDto>(person));

        }

        [HttpPost]
        public async Task<IActionResult> KisiOlustur(PersonDto personDto)
        {
            var newPerson = await _personService.AddAsync(_mapper.Map<MT.MicroService.Core.Entity.Person>(personDto));
            return Created(string.Empty,_mapper.Map<PersonDto>(newPerson));

        }

        [HttpDelete]
        public  IActionResult KisiKaldir(int id) // exception mekanizması ele alınmadı.
        {
            var person = _personService.GetByIdAsync(id).Result;
            _personService.Delete(person);
            return NoContent();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> KisiyiİletisimBilgisiIleGetir(int id) // exception mekanizması ele alınmadı.
        {
            var person = await _personService.GetWithContactInfoByPersonIdAsync(id);

            return Ok(_mapper.Map<PersonReadContactInfoDto>(person));

        }
    }
}
