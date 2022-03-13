using DBLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreLogicLibrary;
using System;
using System.Threading.Tasks;

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
        public void AddNewEmployeeToDB([FromBody] EmployeeDTO employee)
        {
            db.Create(employee);
        }

        [HttpGet("{id}")]
        public Task<Employee> Get([FromRoute] int id)
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

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult Refresh()
        {
            string oldToken = Request.Cookies["refreshToken"];
            var employee = db.GetByToken(oldToken);
            if (employee == null)
            {
                return BadRequest("Re-enter your login and password");
            }
            TokenResponce responce = GenerateTokenResponce(employee.Login, employee.Password, db);
            SetTokenCookie(responce.RefreshToken);
            return Ok(responce);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult AuthenticateEmployee ([FromQuery] string login, string password)
        {
            TokenResponce responce = GenerateTokenResponce(login, password, db);
            if (responce != null)
            {
                SetTokenCookie(responce.RefreshToken);
                return Ok(responce);
            }
            else
            {
                return BadRequest();
            }
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private TokenResponce? GenerateTokenResponce(string login, string password, IEmployeeDBRepository employeeDBRepository)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            var user = employeeDBRepository.GetByLogin(login);
            if (user == null || user.Password != password)
            {
                return null;
            }

            TokenResponce tokenResponce = new ();
            var key = Startup.keyString;
   
            tokenResponce.RefreshToken = tokenResponce.GenerateToken(login, key, TimeSpan.FromMinutes(15));
            tokenResponce.AccessToken = tokenResponce.GenerateToken(login, key, TimeSpan.FromMinutes(1));
            user.Token = tokenResponce.RefreshToken;
            db.Update(user);

            return tokenResponce;
        }

    }
}
