using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scratch.Data;
using Scratch.Models;

namespace Scratch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        DataContext _context;
        IPeopleRepository _peopleRepository;
        public IMapper mapper { get; set; }

        public PeopleController(DataContext context, IPeopleRepository peopleRepository)
        {
            _context = context;
            _peopleRepository = peopleRepository;
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PersonDTO, Person>();
            }));
        }

        // GET: api/People
        [HttpGet("getpersons")]
        public  ActionResult<IEnumerable<Person>> GetPersons()
        {
            return _peopleRepository.GetPersons(this);
        }

        // GET: api/People/5
        [HttpGet("getpersons/{id}")]
        public  ActionResult<Person> GetPerson(int id)
        {
          return _peopleRepository.GetPerson(this, id);
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("putperson/{id}")]
        public IActionResult PutPerson(int id, Person person)
        {
            return _peopleRepository.PutPerson(this, id, person);
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("postperson")]
        public  ActionResult<Person> PostPerson(PersonDTO personDTO)
        {
          return _peopleRepository.PostPerson(this, personDTO);
        }

        [HttpPost("seeddata")]
        public ActionResult SeedData()
        {
            return _peopleRepository.SeedData(this);
        }

        // DELETE: api/People/5
        [HttpDelete("deleteperson/{id}")]
        public IActionResult DeletePerson(int id)
        {
           return _peopleRepository.DeletePerson(this, id);
        }


    }
}
