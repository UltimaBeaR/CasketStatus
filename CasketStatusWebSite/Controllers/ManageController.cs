using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace CasketStatusWebSite.Controllers
{
    // Контроллер для админки
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        //
        // GET: /Manage/Index
        public ActionResult Index()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            /*if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }*/

            // 08.06.2016 BeaR: Поменял на аналогичный AccountController-у вариант (_signInManager поидее тоже должен диспозиться)
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
            }

            base.Dispose(disposing);
        }
    }
}