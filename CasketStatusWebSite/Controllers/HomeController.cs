using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasketStatusWebSite.Models;
using CasketStatusWebSite.Logic;

namespace CasketStatusWebSite.Controllers
{
    // Контроллер для основных страниц (Пока только главная страница со статусом)
    public class HomeController : Controller
    {
        // Репозиторий с текущим статусом работы casket
        ICasketStatusRepository _casketStatusRepository;

        public HomeController()
        {
            // ToDo: Вместо этого поидее потом можно сделать инъекцию зависимостей
            _casketStatusRepository = new CasketStatusEFRepository();
        }

        // Основная страница, показывающая статус работы сервиса Ларец (casket)
        public ActionResult Index()
        {
            CurrentStatusViewModel viewModel = new CurrentStatusViewModel();

            // Получаем текущий статус из БД
            var currentStatus = _casketStatusRepository.CurrentStatus;

            viewModel.StatusCode = currentStatus.Code;
            viewModel.ScheduledWorkDate = currentStatus.ScheduledWorkDate;

            return View(viewModel);
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