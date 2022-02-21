using DBLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeSheets.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserDBRepository db;
        public UsersController(IUserDBRepository repository)
        {
            db = repository;
        }

        [HttpPost]
        public void AddNewUserToDB([FromBody] User user)
        {
            db.Create(user);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(db.Get(id).Result);
        }

        [HttpPut]
        public void Edit([FromBody] User user)
        {
            if (user != null)
            {
                db.Update(user);
            }
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
            db.Delete(id);
        }


    }
}
