using GrantRequests.WEB.Models.Account;
using GrantRequests.WEB.Services;
using System.Web.Mvc;

namespace GrantRequests.WEB.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController(AccountService accountService)
        {
            this.accountService = accountService;
        }
        private readonly AccountService accountService;
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
                if (accountService.LogInUser(model.Name, model.Password))
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
            model.Password = string.Empty;
            return View(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                accountService.RegisterUser(model);
                accountService.LogInUser(model.Name, model.Password);
                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");
            model.Password = string.Empty;
            return View(model);
        }

        public ActionResult Logoff()
        {
            accountService.LogOffUser();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public JsonResult ValidateUserName(string Name)
        {
            if (accountService.IsUserExists(Name))
                return Json("Пользователь с таким логином уже существует",
                    JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}


