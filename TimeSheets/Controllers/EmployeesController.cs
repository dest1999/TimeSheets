using DBLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TimeSheets.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeDBRepository db;
        public EmployeesController(IEmployeeDBRepository repository)
        {
            db = repository;
        }

        [HttpPost]
        public void AddNewEmployeeToDB([FromBody] Employee employee)
        {
            db.Create(employee);
        }

        [HttpGet("{id}")]
        public Task Get([FromRoute] int id)
        {
            return db.Get(id);
        }

        [HttpPut]
        public void Edit([FromBody] Employee employee)
        {
            if (employee != null)
            {
                db.Update(employee);
            }
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
            db.Delete(id);
        }
    }
}
