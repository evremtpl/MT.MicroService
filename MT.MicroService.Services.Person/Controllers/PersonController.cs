using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.MicroService.Core.Services;
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

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpGet]
        public async Task<IActionResult> KisileriListele()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(persons);

        }
    }
}
