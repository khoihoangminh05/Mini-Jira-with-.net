using System.Web.Mvc;
using PersonalTaskManager.Infrastructure.Data;

namespace PersonalTaskManager.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        /// <summary>Dashboard sau đăng nhập.</summary>
        public ActionResult Index()
        {
            ViewBag.Username = CurrentUsername;
            ViewBag.Phase = "Phase 3 — Project CRUD";
            return View();
        }

        /// <summary>Kiểm tra kết nối Oracle.</summary>
        public ActionResult About()
        {
            string message;
            ViewBag.DatabaseConnected = DatabaseConnectionTester.TryConnect(out message);
            ViewBag.DatabaseMessage = message;
            return View();
        }
    }
}
