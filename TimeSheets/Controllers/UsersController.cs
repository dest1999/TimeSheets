using DBLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeSheets.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserDBRepository _db;
        public UsersController(IUserDBRepository repository)
        {
            _db = repository;
        }

        [HttpPost]
        public void AddNewUserToDB([FromBody] User user)
        {
            _db.Create(user);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_db.Get(id) );
        }

        [HttpPut]
        public void EditUser([FromBody] User user)
        {
            if (user != null)
            {
                _db.Update(user);
            }
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
            _db.Delete(id);
        }


    }
}
