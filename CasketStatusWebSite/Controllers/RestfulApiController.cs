using CasketStatusWebSite.Logic;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace CasketStatusWebSite.Controllers
{
    // Контроллер для мобильного api
    [Route("Api")]
    public class RestfulApiController : Controller
    {
        // Репозиторий с текущим статусом работы casket
        ICasketStatusRepository _casketStatusRepository;

        public RestfulApiController()
        {
            _casketStatusRepository = new CasketStatusEFRepository();
        }

        // Получение статуса. GET http://hostname/api/status
        // Ответ приходит в виде Json с текущим статусом
        public ActionResult Status()
        {
            // Получаем текущий статус работы casket
            var currentStatus = _casketStatusRepository.CurrentStatus;

            // Формируем объект json

            object jsonObj;
            switch (currentStatus.Code)
            {
                case StatusCode.WorkScheduled:
                    {
                        jsonObj = new {
                            StatusCode = (int)currentStatus.Code,
                            ScheduledWorkDate = currentStatus.ScheduledWorkDate
                        };
                        break;
                    }
                case StatusCode.Unavailable:
                case StatusCode.Available:
                default:
                    {
                        jsonObj = new { StatusCode = (int)currentStatus.Code };
                        break;
                    }
            }

            // Сериализуем Json через JsonConvert. и возвращаем в виде application/json.
            // Вариант с JsonResult тут не заработал, так как дата в нем получается в стремном виде
            return Content(JsonConvert.SerializeObject(jsonObj), "application/json");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_casketStatusRepository != null)
                {
                    _casketStatusRepository.Dispose();
                    _casketStatusRepository = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}