using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.MicroService.Core.Entity;
using MT.MicroService.Core.Services;
using MT.MicroService.Services.Person.Dtos;
using MT.MicroService.Services.Person.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.MicroService.Services.Person.Controllers
{
    [Route("api/[controller]/[action]")] // best practise açısından method ismi verilmemeli, ancak bu proje için uygulanmıştır.
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IService<Report> _reportService;
        private readonly IMapper _mapper;
        private readonly RabbitMQPublisher _rabbitMQPublisher; 
        public PersonController(IPersonService personService, IService<Report> reportService, IMapper mapper,
            RabbitMQPublisher rabbitMQPublisher)
        {
            _personService = personService;
            _reportService = reportService;
            _mapper = mapper;
            _rabbitMQPublisher = rabbitMQPublisher;
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

        [HttpDelete ("{id}")]
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


        [HttpGet("{id}")]
        public async Task<IActionResult> RaporTalebi(int id) 
        {
            var person = _personService.GetByIdAsync(id).Result;

            Report report = new()
            {
                UUID = person.UUID,
                ReportState = FileStatus.Creating,
                RequestDate = DateTime.Now,
               
            };
            var newReport = await _reportService.AddAsync(report);

            ////burada RAbbitmq
            ///
            _rabbitMQPublisher.Publish(new CreateFileMessage
            {
                FileId = report.id,
                UserId = report.UUID
            });
            return Created(String.Empty,newReport);

        }
        [HttpGet]
        public async Task<IActionResult> RaporlariGetir()
        {
            var reports = await _reportService.GetAllAsync();

            return Ok(reports);
        }
    }
}
