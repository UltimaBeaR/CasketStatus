using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using CasketStatusWebSite.Logic;
using CasketStatusWebSite.Models;

namespace CasketStatusWebSite.Controllers
{
    // Контроллер для админки
    [Authorize]
    public class AdminPanelController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        // Репозиторий с текущим статусом работы casket
        ICasketStatusRepository _casketStatusRepository;

        private void InstantiateRepository()
        {
            _casketStatusRepository = new CasketStatusEFRepository();
        }

        public AdminPanelController()
        {
            InstantiateRepository();
        }

        public AdminPanelController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;

            InstantiateRepository();
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // Основная страница админки
        [HttpGet]
        public ActionResult Index()
        {
            CurrentStatusViewModel viewModel = new CurrentStatusViewModel();

            // Получаем текущий статус из БД
            var currentStatus = _casketStatusRepository.CurrentStatus;

            viewModel.StatusCode = currentStatus.Code;
            viewModel.ScheduledWorkDate = currentStatus.ScheduledWorkDate;

            return View(viewModel);
        }

        // Основная страница админки: изменение текущего статуса
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_UpdateCurrentStatus(CurrentStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (!ModelState.IsValidField(nameof(CurrentStatusViewModel.ScheduledWorkDate)))
                    ModelState.AddModelError("", $"Дата была введена неверно");
                else
                    throw new HttpRequestValidationException();
                   
                return View(model);
            }

            var now = DateTime.Now;
            if (model.StatusCode == StatusCode.WorkScheduled && model.ScheduledWorkDate < now)
            {
                ModelState.AddModelError("", $"Дата запланированных работ не может быть раньше текущего момента времени (Сейчас {now})");
                return View(model);
            }

            // Если все ок, обновляем текущий статус
            _casketStatusRepository.CurrentStatus = new CurrentStatus() { Code = model.StatusCode, ScheduledWorkDate = model.ScheduledWorkDate };

            ViewBag.CurrentStatusUpdated = true; //< Чтобы вьюшка видела, что статус был обновлен
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }

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