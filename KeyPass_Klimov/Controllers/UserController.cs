using KeyPass_Klimov.Classes;
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
                Models.User? AuthUser = databaseManager.Users
                    .Where(x => x.Login == login && x.Password == password)
                    .FirstOrDefault();

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
    }
}
