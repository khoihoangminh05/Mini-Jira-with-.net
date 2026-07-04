using System.Web.Mvc;
using PersonalTaskManager.Infrastructure.Data;

namespace PersonalTaskManager.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>Trang chủ placeholder — Phase 1.</summary>
        public ActionResult Index()
        {
            ViewBag.Phase = "Phase 1 — Scaffold + Database";
            return View();
        }

        /// <summary>Kiểm tra kết nối Oracle (tùy chọn khi đã cấu hình DB).</summary>
        public ActionResult About()
        {
            string message;
            ViewBag.DatabaseConnected = DatabaseConnectionTester.TryConnect(out message);
            ViewBag.DatabaseMessage = message;
            return View();
        }
    }
}
