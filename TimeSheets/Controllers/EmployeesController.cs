using DBLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreLogicLibrary;
using System;

namespace TimeSheets.Controllers
{
    [Authorize]
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
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(db.Get(id).Result);
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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult AuthenticateEmployee ([FromQuery] string user, string password)
        {

            var responce = Authenticate(user, password);
            if (responce != null)
            {
                return Ok(responce);
            }
            else
            {
                return BadRequest();
            }
        }

        private TokenResponce? Authenticate(string login, string password)
        {

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            var user = db.GetByLoginAsync(login);
            if (user == null || user.Password != password)
            {
                return null;
            }

            TokenResponce token = new ();
            var key = Startup.keyString;
   
            token.RefreshToken = token.GenerateToken(login, key, TimeSpan.FromMinutes(15));
            token.AccessToken = token.GenerateToken(login, key, TimeSpan.FromMinutes(1));
            user.Token = token.RefreshToken;
            db.Update(user);

            return token;
        }

    }
}
