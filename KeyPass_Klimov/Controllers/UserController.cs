using BCrypt.Net;
using KeyPass_Klimov.Classes;
using KeyPass_Klimov.Models;
using Microsoft.AspNetCore.Mvc;

namespace KeyPass_Klimov.Controllers
{
    [Route("/user")]
    public class UserController : Controller
    {
        /// <summary>
        /// Приватное поле для хранения экземпляра DatabaseManager
        /// Используется дял работы с базой данных
        /// </summary>
        private DatabaseManager databaseManager;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        public UserController()
        {
            this.databaseManager = this.databaseManager = new DatabaseManager();
        }

        /// <summary>
        /// Метод для аутентификации пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>JWT токен или код ошибки</returns>
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromForm] string login, [FromForm] string password)
        {
            try
            {
                User? AuthUser = databaseManager.Users
                    .FirstOrDefault(x => x.Login == login);

                if (!BCrypt.Net.BCrypt.Verify(password, AuthUser.Password))
                {
                    return StatusCode(401);
                }

                if (AuthUser == null)
                {
                    return StatusCode(401);
                }
                else
                {
                    string Token = JwtToken.Generate(AuthUser);
                    AuthUser.LastAuth = DateTime.Now;
                    databaseManager.SaveChanges();
                    return Ok(new { token = Token });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }

        /// <summary>
        /// Метод для регистрации пользователя
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="surname">Фамилия пользователя</param>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>JWT токен или код ошибки</returns>
        [Route("registration")]
        [HttpPost]
        public ActionResult Registration([FromForm] string name, [FromForm] string surname, [FromForm] string login, [FromForm] string password)
        {
            try
            {
                User? User = databaseManager.Users.FirstOrDefault(x => x.Login == login);

                if (User != null)
                {
                    return StatusCode(400);
                }

                var newUser = new User
                {
                    Name = name,
                    Surename = surname,
                    Login = login,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    LastAuth = DateTime.Now
                };

                databaseManager.Users.Add(newUser);
                databaseManager.SaveChanges();

                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }
    }
}
