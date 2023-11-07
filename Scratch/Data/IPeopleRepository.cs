using Microsoft.AspNetCore.Mvc;
using Scratch.Controllers;
using Scratch.Models;

namespace Scratch.Data
{
    public interface IPeopleRepository
    {
        public bool SaveChanges();
        public void AddEntity<T>(T entity);
        public void RemoveEntity<T>(T entity);
        public ActionResult<IEnumerable<Person>> GetPersons(PeopleController controller);
        public ActionResult<Person> GetPerson(PeopleController controller, int id);
        public IActionResult PutPerson(PeopleController controller, int id, Person person);
        public ActionResult<Person> PostPerson(PeopleController controller, PersonDTO personDTO);
        public ActionResult SeedData(PeopleController controller);
        public IActionResult DeletePerson(PeopleController controller, int id);
    }
}
