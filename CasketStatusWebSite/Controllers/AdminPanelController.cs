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
        public ActionResult Index()
        {
            CurrentStatusViewModel viewModel = new CurrentStatusViewModel();

            // Получаем текущий статус из БД
            var currentStatus = _casketStatusRepository.CurrentStatus;

            viewModel.StatusCode = currentStatus.Code;
            viewModel.ScheduledWorkDate = currentStatus.ScheduledWorkDate;

            return View(viewModel);
        }

        // ToDo: узнать, как передать сложные данные в параметре
        /*
        [HttpPost]
        // Изменение статуса
        public ActionResult ChangeStatus(CurrentStatusViewModel changedStatus)
        {
            //_casketStatusRepository.CurrentStatus = new CurrentStatus() { Code = StatusCode.WorkScheduled, ScheduledWorkDate = System.DateTime.Now };

            return View("Index", changedStatus);
        }
        */

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