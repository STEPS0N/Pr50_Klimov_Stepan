using KeyPass_Klimov.Classes;
using KeyPass_Klimov.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace KeyPass_Klimov.Controllers
{
    /// <summary>
    /// Контроллер для управления хранилищами паролей пользователя
    /// </summary>
    [Route("/storage")]
    public class StorageController : Controller
    {
        private DatabaseManager databaseManager;

        public StorageController()
        {
            this.databaseManager = new DatabaseManager();
        }

        /// <summary>
        /// Получение всех записей хранилища для авторизованного пользователя
        /// </summary>
        /// <param name="token">JWT токен из заголовка запроса</param>
        /// <returns>Список записей хранилища в формате DTO (без информации о пользователе)</returns>
        [Route("get")]
        [HttpGet]
        public ActionResult Get([FromHeader] string token)
        {
            try
            {
                int? IdUser = JwtToken.GetUserIdFromToken(token);

                if (IdUser == null)
                {
                    return StatusCode(401);
                }

                List<StorageDto> Storages = databaseManager.Storages
                    .Where(x => x.User.Id == IdUser)
                    .Select(s => new StorageDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Url = s.Url,
                        Login = s.Login,
                        Password = s.Password,
                    }).ToList();
                return Ok(Storages);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }

        /// <summary>
        /// Добавление новой записи в хранилище
        /// </summary>
        /// <param name="token">JWT токен из заголовка</param>
        /// <param name="storage">Данные новой записи (JSON в теле запроса)</param>
        /// <returns>Добавленная запись</returns>
        [Route("add")]
        [HttpPost]
        public ActionResult Add([FromHeader] string token, [FromBody] Storage storage)
        {
            try
            {
                int? IdUser = JwtToken.GetUserIdFromToken(token);

                if (IdUser == null)
                {
                    return StatusCode(401);
                }

                storage.User = databaseManager.Users.Where(x => x.Id == IdUser).First();

                databaseManager.Add(storage);
                databaseManager.SaveChanges();

                storage.User = null;
                storage.Password = null;

                return StatusCode(200, storage);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }

        /// <summary>
        /// Обновление сущностей записи
        /// </summary>
        /// <param name="token">JWT токен из заголовка</param>
        /// <param name="storage">Обновлённые данные записи</param>
        /// <returns>Обновлённая запись</returns>
        [Route("update")]
        [HttpPut]
        public ActionResult Update([FromHeader] string token, [FromBody] Storage storage)
        {
            try
            {
                int? IdUser = JwtToken.GetUserIdFromToken(token);

                Storage? uStorage = databaseManager.Storages.Where(x => x.Id == storage.Id)
                    .FirstOrDefault();

                if (IdUser == null)
                {
                    return StatusCode(401);
                }

                if (uStorage == null)
                {
                    return StatusCode(404);
                }

                uStorage.Name = storage.Name;
                uStorage.Url = storage.Url;
                uStorage.Password = storage.Password;
                uStorage.Login = storage.Login;

                databaseManager.SaveChanges();

                storage.User = null;
                return StatusCode(200, storage);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }

        /// <summary>
        /// Удаление записи из хранилища
        /// </summary>
        /// <param name="token">JWT токен из заголовка</param>
        /// <param name="id">ID удаляемой записи (из формы)</param>
        /// <returns>Статус выполнения операции</returns>
        [Route("delete")]
        [HttpDelete]
        public ActionResult Delete([FromHeader] string token, [FromForm] int id)
        {
            try
            {
                int? IdUser = JwtToken.GetUserIdFromToken(token);

                Storage? Storage = databaseManager.Storages
                    .Where(x => x.Id == id && x.User.Id == IdUser)
                    .FirstOrDefault();

                if (IdUser == null)
                {
                    return StatusCode(401);
                }

                if (Storage == null)
                {
                    return StatusCode(404);
                }

                databaseManager.Storages.Remove(Storage);
                databaseManager.SaveChanges();

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }
    }
}
