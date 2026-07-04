using System.Web.Mvc;
using System.Web.Security;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Repositories;
using PersonalTaskManager.Infrastructure.Security;
using PersonalTaskManager.Web.Models.Account;

namespace PersonalTaskManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController()
        {
            _userRepository = new UserRepository();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.GetByUsername(model.Username);
            if (user == null || !PasswordHasher.VerifyPassword(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
                return View(model);
            }

            // Forms Authentication cookie + Session cho UserId (dùng filter LINQ phase sau)
            FormsAuthentication.SetAuthCookie(user.Username, model.RememberMe);
            Session[SessionKeys.UserId] = user.UserId;
            Session[SessionKeys.Username] = user.Username;

            return RedirectToLocal(returnUrl);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new RegisterViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_userRepository.UsernameExists(model.Username))
            {
                ModelState.AddModelError(nameof(model.Username), "Tên đăng nhập đã tồn tại.");
            }

            if (_userRepository.EmailExists(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Email đã được sử dụng.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var passwordHash = PasswordHasher.HashPassword(model.Password);
            _userRepository.Create(model.Username, model.Email, passwordHash);

            TempData["SuccessMessage"] = "Đăng ký thành công. Vui lòng đăng nhập.";
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult IsUsernameAvailable(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(!_userRepository.UsernameExists(username.Trim()), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult IsEmailAvailable(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(!_userRepository.EmailExists(email.Trim()), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
