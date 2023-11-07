using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scratch.Controllers;
using Scratch.Models;

namespace Scratch.Data
{
    public class PeopleRepository:IPeopleRepository
    {
        DataContext _context;

        public PeopleRepository(DataContext ef)
        {
            _context = ef;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void AddEntity<T>(T entity)
        {
            if (entity != null)
            {
                _context.Add(entity);
            }

        }

        public void RemoveEntity<T>(T entity)
        {
            if (entity != null)
            {
                _context.Remove(entity);
            }

        }

        public ActionResult<IEnumerable<Person>> GetPersons(PeopleController controller)
        {
            if (_context.Persons == null)
            {
                return controller.NotFound();
            }
            return _context.Persons.ToList();
        }

        public ActionResult<Person> GetPerson(PeopleController controller, int id)
        {
            if (_context.Persons == null)
            {
                return controller.NotFound();
            }
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return controller.NotFound();
            }

            return person;
        }

        public IActionResult PutPerson(PeopleController controller, int id, Person person)
        {
            if (id != person.PersonID)
            {
                return controller.BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return controller.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return controller.NoContent();
        }

        public ActionResult<Person> PostPerson(PeopleController controller, PersonDTO personDTO)
        {
            if (_context.Persons == null)
            {
                return controller.Problem("Entity set 'DataContext.Persons'  is null.");
            }
            Person person = controller.mapper.Map<Person>(personDTO);
            AddEntity<Person>(person);
            SaveChanges();

            return controller.CreatedAtAction("getperson", new { id = person.PersonID }, person);
        }

        public ActionResult SeedData(PeopleController controller)
        {
            int count = _context.Persons.Count();
            if (count > 1) { return controller.Problem("Too many entries for seeding"); }

            for (int i = 0; i < 100; i++)
            {
                AddEntity<Person>(new Person
                {
                    FirstName = "testfirst" + i.ToString(),
                    LastName = "testlast" + i.ToString(),
                    Address = "testaddress" + i.ToString(),
                    City = "testcity" + i.ToString()
                });
                SaveChanges();
            }
            if (_context.Persons.Count() - count == 100)
            {
                return controller.CreatedAtAction("getpersons", _context.Persons); ;
            }
            return controller.Problem("Something went wrong seeding the data...");
        }

        public IActionResult DeletePerson(PeopleController controller, int id)
        {
            if (_context.Persons == null)
            {
                return controller.NotFound();
            }
            var person = _context.Persons.Find(id);
            if (person == null)
            {
                return controller.NotFound();
            }

            RemoveEntity<Person>(person);
            SaveChanges();

            return controller.NoContent();
        }

        private bool PersonExists(int id)
        {
            return (_context.Persons?.Any(e => e.PersonID == id)).GetValueOrDefault();
        }
    }
}
