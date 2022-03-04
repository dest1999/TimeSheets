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

        [HttpPost("refresh-token")]
        public IActionResult Refresh()
        {
            string oldToken = Request.Cookies["refreshToken"];
            string newToken;

            SetTokenCookie(newToken);

        }



        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult AuthenticateEmployee ([FromQuery] string user, string password)
        {
            TokenResponce responce = GenerateTokenResponce(user, password, db);
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
            var user = employeeDBRepository.GetByLoginAsync(login);
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
