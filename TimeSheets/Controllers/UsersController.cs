using DBLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        {//TODO не используй в контроллере модели для базы, у них разное назначение, для передачи данных нужно создать отдельную модель и маппить на нее в контроллере
            db.Create(user);
        }

        [HttpGet("{id}")]
        public Task Get([FromRoute] int id)
        {
            return db.Get(id);
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
