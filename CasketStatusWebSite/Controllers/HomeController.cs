using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasketStatusWebSite.Controllers
{
    // Контроллер для основных страниц (Пока только главная страница со статусом)
    public class HomeController : Controller
    {
        // Основная страница, показывающая статус работы сервиса Ларец (casket)
        public ActionResult Index()
        {
            return View();
        }
    }
}