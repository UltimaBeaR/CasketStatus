using CasketStatusWebSite.Logic;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Collections.Generic;

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
        // Ответ приходит в виде content-type=application/json с текущим статусом
        [ActionName("status")]
        [HttpGet]
        public ActionResult Status(bool? description)
        {
            bool needsDescription = description ?? false;

            // Получаем текущий статус работы casket
            var currentStatus = _casketStatusRepository.CurrentStatus;

            // Формируем объект json

            Dictionary<string, object> jsonObj = new Dictionary<string, object>();

            jsonObj["code"] = (int)currentStatus.Code;

            if (currentStatus.Code == StatusCode.WorkScheduled)
                jsonObj["scheduledWorkDate"] = currentStatus.ScheduledWorkDate;

            if (needsDescription)
            {
                jsonObj["text"] = currentStatus.Code.GetHumanReadableText();
                
                if (currentStatus.Code == StatusCode.WorkScheduled)
                    jsonObj["detailedText"] = string.Format(currentStatus.Code.GetHumanReadableText(true), currentStatus.ScheduledWorkDate);
                else
                    jsonObj["detailedText"] = currentStatus.Code.GetHumanReadableText(true);
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