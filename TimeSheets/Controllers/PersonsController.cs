using DBLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private IDBRepository _dBRepository;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(IDBRepository dBRepository, ILogger<PersonsController> logger)
        {
            _logger = logger;
            _dBRepository = dBRepository;
        }

        [HttpGet("{id}")]
        public Person Get([FromRoute] int id)
        {
            //var 
            return _dBRepository.Read(id);
        }

        [Route("search")]
        [HttpGet]
        public IActionResult Get([FromQuery(Name = "searchTerm")] string term)
        {
            return Ok(_dBRepository.Find(term));
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int skip, [FromQuery] int take)
        {//GET /persons/?skip={5}&take={10} — получение списка людей с пагинацией
            return Ok(_dBRepository.Read(skip, take));
        }

        [HttpPost]
        public void AddNewPersonToDB([FromBody] Person person)
        {//POST /persons — добавление новой персоны в коллекцию
            _dBRepository.Create(person);
        }

        [HttpPut]
        public void EditPerson([FromBody] Person person)
        {//PUT /persons — обновление существующей персоны в коллекции
            _dBRepository.Update(person);
        }


        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {//DELETE /persons/{id} — удаление персоны из коллекции
            _dBRepository.Delete(id);
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {//just for testing
         //https://localhost:44362/Persons/getall
            return Ok(_dBRepository.GetAll());
        }

    }
}
