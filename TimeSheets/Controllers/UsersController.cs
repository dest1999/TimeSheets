using DBLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TimeSheets.Controllers
{
    [Authorize]
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
        public void AddNewUserToDB([FromBody] UserDTO user)
        {
            db.Create(user);
        }

        [HttpGet("{id}")]
        public Task<User> Get([FromRoute, Required, Range(1, int.MaxValue)] int id)
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
        public void Delete([FromRoute, Required, Range(1, int.MaxValue)] int id)
        {
            db.Delete(id);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult Refresh()
        {
            string oldToken = Request.Cookies["refreshToken"];
            var user = db.GetByToken(oldToken);
            if (user == null)
            {
                return BadRequest("Re-enter your login and password");
            }
            TokenResponce responce = GenerateTokenResponce(user.Login, user.Password, db);
            SetTokenCookie(responce.RefreshToken);
            return Ok(responce);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult AuthenticateEmployee([FromQuery] string login, string password)
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

        private TokenResponce? GenerateTokenResponce(string login, string password, IUserDBRepository userDBRepository)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            var user = userDBRepository.GetByLogin(login);
            if (user == null || user.Password != password)
            {
                return null;
            }

            TokenResponce tokenResponce = new();
            var key = Startup.keyString;

            tokenResponce.RefreshToken = tokenResponce.GenerateToken(login, key, TimeSpan.FromMinutes(15));
            tokenResponce.AccessToken = tokenResponce.GenerateToken(login, key, TimeSpan.FromMinutes(1));
            user.Token = tokenResponce.RefreshToken;
            db.Update(user);

            return tokenResponce;
        }
    }
}
